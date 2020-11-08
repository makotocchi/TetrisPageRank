using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using WinApiWrapper.Native;
using static WinApiWrapper.Native.NativeEnums;

namespace KeyBloxManager
{
    public class MemoryReader
    {
        private readonly SafeProcessHandle processHandle;

        public MemoryReader(string processName)
        {
            var process = Process.GetProcessesByName(processName).SingleOrDefault();
            processHandle = NativeMethods.OpenProcess(ProcessAccessFlags.VirtualMemoryRead, false, process.Id);

            if (processHandle == null)
            {
                throw new ArgumentException(Marshal.GetLastWin32Error().ToString(CultureInfo.InvariantCulture), nameof(processName));
            }
        }

        public T ReadProcessMemory<T>(IntPtr address) where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var buffer = new byte[size];

            var success = NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out int bytesRead);

            if (!success || bytesRead != size)
            {
                throw new Exception(Marshal.GetLastWin32Error().ToString(CultureInfo.InvariantCulture));
            }

            return ByteArraytoStruct<T>(buffer);
        }

        private static T ByteArraytoStruct<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }
    }
}