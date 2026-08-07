using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace adisyon.Printers
{
    public class EscPosPrinter
    {
        #region WinAPI

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true)]
        private static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, int level, DOCINFO di);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool WritePrinter(
            IntPtr hPrinter,
            IntPtr pBytes,
            int dwCount,
            out int dwWritten);

        [StructLayout(LayoutKind.Sequential)]
        public struct DOCINFO
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        #endregion

        private readonly string _printerName;

        // Yazıcıya gitmeden önce bütün veriler burada birikecek
        private readonly List<byte> _buffer = new List<byte>();

        public EscPosPrinter(string printerName)
        {
            _printerName = printerName;
        }

        private void Add(byte value)
        {
            _buffer.Add(value);
        }

        private void Add(byte[] values)
        {
            _buffer.AddRange(values);
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        public void Print()
        {
            if (_buffer.Count == 0)
                return;

            if (!OpenPrinter(_printerName, out IntPtr hPrinter, IntPtr.Zero))
                throw new Exception("Yazıcı açılamadı.");

            DOCINFO di = new DOCINFO
            {
                pDocName = "ErgPOS",
                pDataType = "RAW"
            };

            if (!StartDocPrinter(hPrinter, 1, di))
            {
                ClosePrinter(hPrinter);
                throw new Exception("Print Job başlatılamadı.");
            }

            StartPagePrinter(hPrinter);

            byte[] bytes = _buffer.ToArray();

            IntPtr unmanagedBytes = Marshal.AllocCoTaskMem(bytes.Length);

            Marshal.Copy(bytes, 0, unmanagedBytes, bytes.Length);

            WritePrinter(
                hPrinter,
                unmanagedBytes,
                bytes.Length,
                out _);

            Marshal.FreeCoTaskMem(unmanagedBytes);

            EndPagePrinter(hPrinter);

            EndDocPrinter(hPrinter);

            ClosePrinter(hPrinter);

            _buffer.Clear();
        }

        #region TEXT

        public void Text(string text)
        {
            _buffer.AddRange(Encoding.GetEncoding(857).GetBytes(text));
        }

        public void NewLine(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Add((byte)'\n');
            }
        }

        #endregion

        #region ALIGN

        public void Left()
        {
            Add(new byte[]
            {
        0x1B, 0x61, 0x00
            });
        }

        public void Center()
        {
            Add(new byte[]
            {
        0x1B, 0x61, 0x01
            });
        }

        public void Right()
        {
            Add(new byte[]
            {
        0x1B, 0x61, 0x02
            });
        }

        #endregion

        #region FONT

        public void Bold(bool enable)
        {
            Add(new byte[]
            {
        0x1B,
        0x45,
        (byte)(enable ? 1 : 0)
            });
        }

        public void Underline(bool enable)
        {
            Add(new byte[]
            {
        0x1B,
        0x2D,
        (byte)(enable ? 1 : 0)
            });
        }

        public void Inverse(bool enable)
        {
            Add(new byte[]
            {
        0x1D,
        0x42,
        (byte)(enable ? 1 : 0)
            });
        }

        public void DoubleSize(bool enable)
        {
            Add(new byte[]
            {
        0x1D,
        0x21,
        (byte)(enable ? 0x11 : 0x00)
            });
        }

        public void DoubleWidth(bool enable)
        {
            Add(new byte[]
            {
        0x1D,
        0x21,
        (byte)(enable ? 0x10 : 0x00)
            });
        }

        public void DoubleHeight(bool enable)
        {
            Add(new byte[]
            {
        0x1D,
        0x21,
        (byte)(enable ? 0x01 : 0x00)
            });
        }

        #endregion

        #region PAPER

        public void Feed(int line = 3)
        {
            NewLine(line);
        }

        public void Cut()
        {
            Add(new byte[]
            {
        0x1D,
        0x56,
        0x00
            });
        }

        #endregion

        #region INIT

        public void Initialize()
        {
            Add(new byte[]
            {
        0x1B,
        0x40
            });
        }

        #endregion
    }
}