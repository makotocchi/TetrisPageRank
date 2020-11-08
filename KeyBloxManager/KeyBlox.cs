using System;
using WinApiWrapper;
using static WinApiWrapper.Native.NativeEnums;

namespace KeyBloxManager
{
    internal class KeyBlox
    {
        private const string PROCESS_NAME = "KeyBlox";

        private readonly MemoryReader _memoryReader;
        private readonly InputSimulator _inputSimulator;
        private readonly int _previewSize;

        public KeyBlox(int previewSize = 10)
        {
            _inputSimulator = new InputSimulator();
            _memoryReader = new MemoryReader(PROCESS_NAME);
            _previewSize = previewSize;
        }

        public KeyBloxPiece GetCurrentPiece() => _memoryReader.ReadProcessMemory<KeyBloxPiece>(new IntPtr(MemoryAddress.CURRENT_PIECE));

        public KeyBloxPiece GetHeldPiece() => _memoryReader.ReadProcessMemory<KeyBloxPiece>(new IntPtr(MemoryAddress.HELD_PIECE));

        public KeyBloxPiece[] GetPreviewedPieces()
        {
            var pieces = new KeyBloxPiece[_previewSize];

            for (int i = 0; i < _previewSize; i++)
            {
                pieces[i] = _memoryReader.ReadProcessMemory<KeyBloxPiece>(new IntPtr(MemoryAddress.PREVIEW_PIECES + KeyBloxPiece.Size * i));
            }

            return pieces;
        }

        public void HoldPiece()
        {
            _inputSimulator.SendKey(ScanCode.SPACE);
        }

        public void RotatePiece(int degrees)
        {
            switch (degrees)
            {
                case 90:
                    _inputSimulator.SendKey(ScanCode.KEY_W);
                    break;
                case 180:
                    _inputSimulator.SendKey(ScanCode.KEY_E);
                    break;
                case 270:
                    _inputSimulator.SendKey(ScanCode.KEY_R);
                    break;
                default:
                    break;
            }
        }

        public void DropPiece(int column)
        {
            ScanCode[] _dropScanCodes = new[]
            {
                ScanCode.KEY_A,
                ScanCode.KEY_S,
                ScanCode.KEY_D,
                ScanCode.KEY_F,
                ScanCode.KEY_G,
                ScanCode.KEY_H,
                ScanCode.KEY_J,
                ScanCode.KEY_K,
                ScanCode.KEY_L,
                ScanCode.OEM_1
            };

            _inputSimulator.SendKey(_dropScanCodes[column]);
        }

        public void DoTetris()
        {
            _inputSimulator.SendKey(ScanCode.KEY_W);
            _inputSimulator.SendKey(ScanCode.OEM_1);
        }
    }
}