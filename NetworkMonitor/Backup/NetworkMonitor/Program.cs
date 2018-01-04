using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace NetworkMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        static void WinINETAPI()
        {
            int flags;
            bool isConnected = WinINET.InternetGetConnectedState(out flags, 0);
            Console.WriteLine(string.Format("Is connected :{0} Flags:{1}", isConnected, ((WinINET.ConnectionStates)flags).ToString()));
        }

        static void NetInfo()
        {
            foreach (NetworkInterface face in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("=================================================");
                Console.WriteLine(face.Id);
                Console.WriteLine(face.Name);
                Console.WriteLine(face.NetworkInterfaceType.ToString());
                Console.WriteLine(face.OperationalStatus.ToString());
                Console.WriteLine(face.Speed);
            }
        }
    }
}
