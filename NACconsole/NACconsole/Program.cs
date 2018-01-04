using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NACconsole
{
    class Program
    {

        private static bool keepRunning = true;

        public static void Main(string[] args)
        {
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
                e.Cancel = true;
                Program.keepRunning = false;
            };

            while (Program.keepRunning)
            {
                // Do your work in here, in small chunks.
                // If you literally just want to wait until ctrl-c,
                // not doing anything, see the answer using set-reset events.
                ShowNetworkTraffic();
            }
            Console.WriteLine("Program Interrupted... ");
            Console.ReadLine();
        }

        private static void ShowNetworkTraffic()
        {
            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0]; // 1st NIC !
            PerformanceCounter performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
            PerformanceCounter performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("bytes sent: {0}k\tbytes received: {1}k", performanceCounterSent.NextValue() / 1024, performanceCounterReceived.NextValue() / 1024);
                Thread.Sleep(500);
            }
        }
    }
}
