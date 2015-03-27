using System;
using RemoteDesktopShutdown.Properties;

namespace RemoteDesktopShutdown
{
    internal static class Session
    {
        private readonly static Win32Api.SYSTEM_POWER_CAPABILITIES SystemPowerCapabilities;

        static Session()
        {
            Win32Api.GetPwrCapabilities(out SystemPowerCapabilities);
        }

        public static void SignOut()
        {
            Win32Api.ExitWindowsEx(Win32Api.EWX_LOGOFF, 0);
        }

        public static void Shutdown()
        {
            if (!AdjustProcessPrivileges())
                throw new InvalidOperationException(Resources.MissingPrivileges);

            Win32Api.ExitWindowsEx(Win32Api.EWX_POWEROFF, 0);
        }

        public static void Restart()
        {
            if (!AdjustProcessPrivileges())
                throw new InvalidOperationException(Resources.MissingPrivileges);

            Win32Api.ExitWindowsEx(Win32Api.EWX_REBOOT, 0);
        }

        private static bool AdjustProcessPrivileges()
        {
            IntPtr hProc = Win32Api.GetCurrentProcess();
            IntPtr hTok = IntPtr.Zero;

            if (!Win32Api.OpenProcessToken(hProc, Win32Api.TOKEN_ADJUST_PRIVILEGES | Win32Api.TOKEN_QUERY, ref hTok))
                return false;

            Win32Api.TOKEN_PRIVILEGES privileges = new Win32Api.TOKEN_PRIVILEGES()
            {
                PrivilegeCount = 1,
                Privileges = new Win32Api.LUID_AND_ATTRIBUTES[] { new Win32Api.LUID_AND_ATTRIBUTES() { Attributes = Win32Api.SE_PRIVILEGE_ENABLED, Luid = new Win32Api.LUID() } }
            };

            if (!Win32Api.LookupPrivilegeValue(null, Win32Api.SE_SHUTDOWN_NAME, out privileges.Privileges[0].Luid))
            {
                Win32Api.CloseHandle(hTok);
                return false;
            }

            bool isSuccessfull = Win32Api.AdjustTokenPrivileges(hTok, false, ref privileges, 0, IntPtr.Zero, IntPtr.Zero);
            Win32Api.CloseHandle(hTok);

            return isSuccessfull;
        }
    }
}