using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using adisyon.Models;
using System.Configuration;

namespace adisyon
{
    public partial class ucUrunKart : UserControl
    {

        public Urun urun { get; private set; }
        public SepetUrun s_urun { get; private set; }

        public event EventHandler<Urun> UrunSecildi;
        public ucUrunKart()
        {
            InitializeComponent();
        }

        public void SetUrun(Urun urun)
        {
            this.urun = urun;
            lblUrunIsim.Text = urun.Ad;
            lblFiyat.Text = urun.Fiyat.ToString("0.00") + " ₺";
            if(urun.Stok <= 0)
                lblStok.ForeColor = Color.Red;
            else if(urun.Stok <= urun.KritikStok)
                lblStok.ForeColor = Color.Orange;
            else
                lblStok.ForeColor = Color.Black;
            lblStok.Text = $"Stok: {urun.Stok}";

            urunResim.ImageLocation = Path.Combine(ConfigurationManager.AppSettings["path"].ToString() , urun.resimYolu);
            string yoll = Path.Combine(ConfigurationManager.AppSettings["path"].ToString(), urun.resimYolu);
            //MessageBox.Show(yoll);
            urunResim.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucUrunKart_Load(object sender, EventArgs e)
        {
            guna2Panel1.BorderRadius = 1;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.BorderColor = Color.LightGray;
            guna2Panel1.FillColor = Color.White;
            guna2Panel1.ShadowDecoration.Enabled = true;
            lblUrunIsim.Width = this.Width;
        }

        private void clicked()
        {
            if(ucSatis.sepethakki == 2 && ucSatis.btnAktif == true)
            {
                template.ShowMessageInfo("Sepet hakkınız dolmuştur. Lütfen önceki sepet(ler)i tamamlayın.", this.FindForm());
                return;
            }
            if (ucSatis.sepethakki == 1 && ucSatis.btnAktif == true)
            {
                template.ShowMessageInfo("Lütfen sağ yukardan 'Yeni sepet' butonuna basarak sepet açın.", this.FindForm());
                return;
            }
            UrunSecildi?.Invoke(this, urun);
        }
        //satış kısmında artık bi satışşsız işlem bide ürün getirme kaldı sonra finito

        private void ucUrunKart_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel1_Click(object sender, EventArgs e)
        {
            clicked();
        }

        private void urunResim_Click(object sender, EventArgs e)
        {
            clicked();
        }

        private void lblUrunIsim_Click(object sender, EventArgs e)
        {
            clicked();
        }

        private void lblFiyat_Click(object sender, EventArgs e)
        {
            clicked();
        }

        private void lblStok_Click(object sender, EventArgs e)
        {
            clicked();
        }
    }

 
}
