using System;
using System.Collections.Generic;
using System.Linq;

namespace adisyon.Printers
{
    public class PrinterManager
    {
        private readonly string _printerName;

        public PrinterManager()
            : this(ReceiptPrinter.GetSelectedPrinterName())
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
            decimal nakitTutar,
            decimal kartTutar,
            decimal kalan,
            decimal paraUstu,
            string altYazi)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            ReceiptPrinter.EnsureSelectedPrinter();

            if (string.IsNullOrWhiteSpace(_printerName))
                return;
                //throw new InvalidOperationException("Fiş yazıcısı seçilmedi.");

            ReceiptBuilder builder = new ReceiptBuilder();
            decimal toplam = 0m;

            builder.Title(firmaAdi);

            if (!string.IsNullOrWhiteSpace(telefon))
                builder.Phone(telefon);

            builder.Line();
            builder.Date(tarih);
            builder.ReceiptNo(fisNo);
            builder.Line();

            foreach (ReceiptLine line in lines)
            {
                builder.Left(line.ad);
                builder.LeftRight(
                    string.Format("{0} x {1:0.00}", line.miktar, line.fiyat),
                    line.toplamFiyat.ToString("0.00"));

                toplam += line.toplamFiyat;
            }

            builder.Total(toplam);

            if (nakitTutar > 0)
                builder.Payment("NAKİT", nakitTutar);

            if (kartTutar > 0)
                builder.Payment("KART", kartTutar);

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
