using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adisyon.Models
{
    public class SepetUrun
    {
        public Urun urun { get; set; }
        public int Adet { get; set; }

        public decimal ToplamFiyat
        {
            get
            {
                return urun.Fiyat * Adet;
            }
        }
    }
}
