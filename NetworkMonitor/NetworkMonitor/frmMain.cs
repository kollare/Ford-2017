using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyNetworkInfo;
using System.Threading;

namespace NetworkMonitor
{
    public partial class frmMain : Form
    {
        Thread _thread;
        bool m_IsAlive;
        MethodInvoker UIInvoke;
        int m_Index;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            m_Index = -1;
            NetworkManager.Instance.StartMonitor();
            m_IsAlive = true;
            UIInvoke = new MethodInvoker(UpdateUI);
            Initial();
            _thread = new Thread(new ThreadStart(UpdateStatus));
            _thread.Start();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            NetworkManager.Instance.Destory();
            m_IsAlive = false;
        }

        void Initial()
        {
            foreach(NetworkInfo info in NetworkManager.Instance.Informations.Values)
            {
                lstAllNetwork.Items.Add(info.ConnectionID);
            }
        }

        void UpdateStatus()
        {
            while(m_IsAlive)
            {
                try
                {
                    Invoke(UIInvoke);
                }catch{}
                Thread.Sleep(300);
            }
        }

        void UpdateUI()
        {
            if (m_Index >= 0)
            {
                string key = lstAllNetwork.Items[lstAllNetwork.SelectedIndex].ToString();
                SetInfornation(NetworkManager.Instance.Informations[key]);
            }
            else
            {
                ClearInformation();
            }
        }

        void SetInfornation(NetworkInfo info)
        {
            lblNetworkName.Text = info.ConnectionID;
            lblDeviceName.Text = info.DeviceName;
            lblAdapterType.Text = info.AdapterType;
            lblMac.Text = info.MacAddress;
            lblIP.Text = info.IP;
            lblMask.Text = info.Mask;
            lblGateway.Text = info.DefaultGateway;
            lblStatus.Text = info.Status.ToString();
            lblHelp.Text = info.GetHelp();
        }

        void ClearInformation()
        {
            lblNetworkName.Text = "";
            lblDeviceName.Text = "";
            lblAdapterType.Text = "";
            lblMac.Text = "";
            lblIP.Text = "";
            lblMask.Text = "";
            lblGateway.Text = "";
            lblStatus.Text = "";
            lblHelp.Text = "";
        }

        private void lstAllNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(lstAllNetwork.SelectedIndex);
            m_Index = lstAllNetwork.SelectedIndex;
        }
    }
}
