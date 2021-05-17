using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using static WinApiWrapper.Native.NativeEnums;
using static WinApiWrapper.Native.NativeStructs;

namespace WinApiWrapper.Native
{
    internal static class NativeMethods
    {
        private const string KERNEL32 = "kernel32.dll";
        private const string USER32 = "user32.dll";

        [DllImport(KERNEL32, SetLastError = true)]
        public static extern SafeProcessHandle OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport(KERNEL32, SetLastError = true)]
        public static extern bool ReadProcessMemory(SafeProcessHandle hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

        [DllImport(USER32, SetLastError = true)]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] Input[] pInputs, int cbSize);

        [DllImport(USER32)]
        public static extern IntPtr GetMessageExtraInfo();

        [DllImport(USER32)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}