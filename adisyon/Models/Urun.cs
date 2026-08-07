using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adisyon.Models
{
    public class Urun
    {
        public int Id { get; set; }
        public string Barkod { get; set; }
        public string Ad { get; set; }
        public string kategori { get; set; }

        public decimal Fiyat { get; set; }

        public int Stok { get; set; }

        public int KritikStok { get; set; }

        public string resimYolu { get; set; }


    }
}
