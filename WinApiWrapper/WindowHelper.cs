using System.Diagnostics;
using System.Linq;
using WinApiWrapper.Native;

namespace WinApiWrapper
{
    public class WindowHelper
    {
        private readonly Process _process;

        public WindowHelper(string processName)
        {
            _process = Process.GetProcessesByName(processName).SingleOrDefault();
        }

        public void BringToFront()
        {
            NativeMethods.SetForegroundWindow(_process.MainWindowHandle);
        }
    }
}
