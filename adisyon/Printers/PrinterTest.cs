using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace adisyon.Printers
{
    public static class PrinterTest
    {
        public static string BuildSampleReceiptText()
        {
            ReceiptBuilder builder = new ReceiptBuilder();

            builder.Title("ADÝSYON TEST");
            builder.Phone("0 (555) 123 45 67");
            builder.Line();
            builder.Date(DateTime.Now);
            builder.ReceiptNo(1);
            builder.Line();

            List<ReceiptLine> items = new List<ReceiptLine>
            {
                new ReceiptLine("Çay", 2, 10m),
                new ReceiptLine("Tost", 1, 75m),
                new ReceiptLine("Kola", 3, 25m)
            };

            foreach (ReceiptLine item in items)
            {
                builder.Left(item.ad);
                builder.LeftRight(
                    string.Format("{0} x {1:0.00}", item.miktar, item.fiyat),
                    item.toplamFiyat.ToString("0.00"));
            }

            decimal toplam = items.Sum(x => x.toplamFiyat);

            builder.Total(toplam);
            builder.Payment("NAKÝT", 100m);
            builder.Payment("KALAN", 5m);
            builder.Footer("TEST FÝÞÝDÝR");

            return builder.Build();
        }

        public static void PreviewSampleReceipt()
        {
            string text = BuildSampleReceiptText();
            Debug.WriteLine(text);
            MessageBox.Show(text, "Fiþ Önizleme");
        }
    }
}

