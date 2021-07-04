using System;
using System.Runtime.InteropServices;
using static WinApiWrapper.Native.NativeEnums;

namespace WinApiWrapper.Native
{
    internal static class NativeStructs
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Input
        {
            public InputTypes type;
            public InputUnion u;

            public static int Size => Marshal.SizeOf(typeof(Input));
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)]
            public MouseInput mi;

            [FieldOffset(0)]
            public KeyboardInput ki;

            [FieldOffset(0)]
            public HardwareInput hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ScanCode wScan;
            public KeyEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }
    }
}