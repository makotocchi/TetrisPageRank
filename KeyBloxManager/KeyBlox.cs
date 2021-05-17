using System;
using TetrisPageRank;
using WinApiWrapper;
using static WinApiWrapper.Native.NativeEnums;

namespace KeyBloxManager
{
    internal class KeyBlox
    {
        private const string PROCESS_NAME = "KeyBlox";

        private readonly MemoryReader _memoryReader = new(PROCESS_NAME);
        private readonly WindowHelper _windowHelper = new(PROCESS_NAME);
        private readonly InputSimulator _inputSimulator = new();
        private readonly int _previewSize;

        public KeyBlox(int previewSize = 10) => _previewSize = previewSize;

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

        public bool IsPlayable()
        {
            int menuSelection = _memoryReader.ReadProcessMemory<int>(new IntPtr(MemoryAddress.MENU_SELECTION));

            if (menuSelection != 0)
            {
                int inputsEnabled = _memoryReader.ReadProcessMemory<int>(new IntPtr(MemoryAddress.INPUTS_ENABLED));

                return inputsEnabled == 1;
            }

            return false;
        }

        public int GetPieceCount() => _memoryReader.ReadProcessMemory<int>(new IntPtr(MemoryAddress.PIECE_COUNT));
        public int GetLineCount() => _memoryReader.ReadProcessMemory<int>(new IntPtr(MemoryAddress.LINE_COUNT));

        public void HoldPiece() => _inputSimulator.SendKey(ScanCode.SPACE);

        public void RotatePiece(int degrees)
        {
            if (degrees == 90)
            {
                _inputSimulator.SendKey(ScanCode.KEY_W);
            }
            else if (degrees == 180)
            {
                _inputSimulator.SendKey(ScanCode.KEY_E);
            }
            else if (degrees == 270)
            {
                _inputSimulator.SendKey(ScanCode.KEY_R);
            }
        }

        public void DropPiece(int column)
        {
            ReadOnlySpan<ScanCode> _dropScanCodes = stackalloc[]
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

        public void Reset()
        {
            _inputSimulator.SendKey(ScanCode.ESCAPE);
            _inputSimulator.SendKey(ScanCode.RETURN);
        }

        public void Focus()
        {
            _windowHelper.BringToFront();
        }

        public void ExecuteDrop(TetrisDrop drop)
        {
            RotatePiece(drop.Orientation);
            DropPiece(drop.Column);
        }
    }
}