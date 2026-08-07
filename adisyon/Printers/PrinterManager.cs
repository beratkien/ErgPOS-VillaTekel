using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace adisyon.Printers
{
    public class PrinterManager
    {
        private readonly string _printerName;

        public PrinterManager()
            : this(ConfigurationManager.AppSettings["ReceiptPrinterName"])
        {
        }

        public PrinterManager(string printerName)
        {
            _printerName = printerName;
        }

        public void PrintSaleReceipt(
            int fisNo,
            DateTime tarih,
            string firmaAdi,
            string telefon,
            IEnumerable<ReceiptLine> lines,
            string odemeTipi,
            decimal odenen,
            decimal kalan,
            decimal paraUstu,
            string altYazi)
        {
            if (lines == null)
                throw new ArgumentNullException("lines");

            if (string.IsNullOrWhiteSpace(_printerName))
                throw new InvalidOperationException("ReceiptPrinterName tanımlı değil.");

            ReceiptBuilder builder = new ReceiptBuilder();

            builder.Title(firmaAdi);
            if (!string.IsNullOrWhiteSpace(telefon))
                builder.Phone(telefon);

            builder.Line();
            builder.Date(tarih);
            builder.ReceiptNo(fisNo);
            builder.Line();

            decimal toplam = 0m;

            foreach (ReceiptLine line in lines)
            {
                builder.Left(line.ad);
                builder.LeftRight(
                    string.Format("{0} x {1:0.00}", line.miktar, line.fiyat),
                    line.toplamFiyat.ToString("0.00"));

                toplam += line.toplamFiyat;
            }

            builder.Total(toplam);

            if (!string.IsNullOrWhiteSpace(odemeTipi))
                builder.Payment(odemeTipi, odenen);

            if (kalan > 0)
                builder.Payment("KALAN", kalan);

            if (paraUstu > 0)
                builder.Payment("PARA ÜSTÜ", paraUstu);

            if (!string.IsNullOrWhiteSpace(altYazi))
                builder.Footer(altYazi);

            EscPosPrinter printer = new EscPosPrinter(_printerName);
            printer.Initialize();
            printer.Text(builder.Build());
            printer.Feed(4);
            printer.Cut();
            printer.Print();
        }
    }
}
