using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace NetworkMonitor
{
    public class WinINET
    {
        [DllImport("wininet.dll", SetLastError = true)]
        public extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        [Flags]
        public enum ConnectionStates
        {
            Modem = 0x1,
            LAN = 0x2,
            Proxy = 0x4,
            RasInstalled = 0x10,
            Offline = 0x20,
            Configured = 0x40,
        }
    }
}
