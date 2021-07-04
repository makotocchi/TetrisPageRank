using System;
using System.Globalization;
using System.Runtime.InteropServices;
using static WinApiWrapper.Native.NativeEnums;
using static WinApiWrapper.Native.NativeMethods;
using static WinApiWrapper.Native.NativeStructs;

namespace WinApiWrapper
{
    public class InputSimulator
    {
        public void SendKey(ScanCode scanCode)
        {
            var inputData = new[]
            {
                new Input
                {
                    type = InputTypes.KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KeyboardInput
                        {
                            wScan = scanCode,
                            dwFlags = KeyEventFlags.SCANCODE,
                            time = 0,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
                new Input
                {
                    type = InputTypes.KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KeyboardInput
                        {
                            wScan = scanCode,
                            dwFlags = KeyEventFlags.KEYUP | KeyEventFlags.SCANCODE,
                            time = 0,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
            };

            if (SendInput(2, inputData, Input.Size) == 0)
            {
                throw new Exception(Marshal.GetLastWin32Error().ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}