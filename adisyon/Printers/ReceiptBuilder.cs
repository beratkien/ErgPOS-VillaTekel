using System;
using System.Text;

namespace adisyon.Printers
{
    public class ReceiptBuilder
    {
        // 58 mm yazıcı
        private const int LINE_LENGTH = 32;

        private readonly StringBuilder _sb = new StringBuilder();

        public string Build()
        {
            return _sb.ToString();
        }

        public void EmptyLine()
        {
            _sb.AppendLine();
        }

        public void Line()
        {
            _sb.AppendLine(new string('-', LINE_LENGTH));
        }

        public void Center(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _sb.AppendLine();
                return;
            }

            if (text.Length >= LINE_LENGTH)
            {
                _sb.AppendLine(text);
                return;
            }

            int left = (LINE_LENGTH - text.Length) / 2;
            _sb.AppendLine(new string(' ', left) + text);
        }

        public void Left(string text)
        {
            _sb.AppendLine(text ?? string.Empty);
        }

        public void Right(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _sb.AppendLine();
                return;
            }

            if (text.Length >= LINE_LENGTH)
            {
                _sb.AppendLine(text);
                return;
            }

            _sb.AppendLine(text.PadLeft(LINE_LENGTH));
        }

        public void LeftRight(string left, string right)
        {
            left = left ?? string.Empty;
            right = right ?? string.Empty;

            if (left.Length + right.Length > LINE_LENGTH)
            {
                _sb.AppendLine(left);
                _sb.AppendLine(right.PadLeft(LINE_LENGTH));
                return;
            }

            int bosluk = LINE_LENGTH - left.Length - right.Length;
            _sb.AppendLine(left + new string(' ', bosluk) + right);
        }

        public void Product(string ad, decimal adet, decimal fiyat)
        {
            decimal toplam = adet * fiyat;

            string qty = string.Format("{0}x{1:0.00}", adet, fiyat);
            string total = toplam.ToString("0.00");

            _sb.AppendLine(ad ?? string.Empty);
            LeftRight(qty, total);
        }

        public void Title(string firma)
        {
            Center(firma);
        }

        public void Phone(string tel)
        {
            Center(tel);
        }

        public void Date(DateTime tarih)
        {
            LeftRight("Tarih", tarih.ToString("dd.MM.yyyy HH:mm"));
        }

        public void ReceiptNo(int no)
        {
            LeftRight("Fiş No", no.ToString());
        }

        public void Total(decimal toplam)
        {
            Line();
            LeftRight("TOPLAM", toplam.ToString("0.00"));
            Line();
        }

        public void Payment(string tip, decimal tutar)
        {
            LeftRight(tip, tutar.ToString("0.00"));
        }

        public void Footer(string text)
        {
            EmptyLine();
            Center(text);
        }
    }
}   