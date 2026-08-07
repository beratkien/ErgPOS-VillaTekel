using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adisyon.Printers
{
    public class ReceiptLine
    {
        public ReceiptLine()
        {
        }

        public ReceiptLine(string ad, int miktar, decimal fiyat)
        {
            this.ad = ad;
            this.miktar = miktar;
            this.fiyat = fiyat;
        }

        public string ad { get; set; }
        public int miktar { get; set; }
        public decimal fiyat { get; set; }

        public decimal toplamFiyat
        {
            get { return miktar * fiyat; }
        }
    }
}
