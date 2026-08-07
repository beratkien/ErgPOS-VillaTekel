using adisyon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace adisyon
{
    public partial class ucSepettekiUrunler : UserControl
    {
        public SepetUrun s_urun { get; private set; }
        
        public event EventHandler UrunSilindi;
        public event EventHandler ToplamArtti;
        public event EventHandler ToplamAzaldi;
        public ucSepettekiUrunler()
        {
            InitializeComponent();
        }

        public void SetUrun(Urun urun, int adet = 1)
        {
            s_urun = new SepetUrun();
            s_urun.urun = urun;
            s_urun.Adet = adet;
            //MessageBox.Show($"Fiyat = {urun.Fiyat}, Adet = {adet}, Toplam = {s_urun.ToplamFiyat}");
            lblS_UrunName.Text = urun.Ad;
            lblS_UrunFiyat.Text = urun.Fiyat.ToString("0.00") + " TL";
            lblS_UrunAdet.Text = s_urun.Adet.ToString();
            lblS_UrunTutar.Text = s_urun.ToplamFiyat.ToString("0.00") + " TL";

        }

        public void AdetGuncelle(int adet = 1)
        { 
            s_urun.Adet += adet;
            lblS_UrunAdet.Text = s_urun.Adet.ToString();
            decimal toplamFiyat = s_urun.ToplamFiyat;
            lblS_UrunTutar.Text = toplamFiyat.ToString("0.00") + " TL";
        }

        public void butonuAc()
        {
            btnAzalt.Enabled = true;
        }
        private void ucSepettekiUrunler_Load(object sender, EventArgs e)
        {
            this.Width = this.Parent.Width - 7;
            if (s_urun.Adet == 1)
                btnAzalt.Enabled = false;
        }

        private void lblS_UrunFiyat_Click(object sender, EventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            s_urun.Adet = 0;
            UrunSilindi?.Invoke(this, EventArgs.Empty);
        }

        private void btnArttir_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand sqlCommand = new SqlCommand("UPDATE aski_1 SET adet=@adet, tutar=@tutar WHERE id=@id", con);
            s_urun.Adet++;
            if (s_urun.Adet > 1)
                btnAzalt.Enabled = true;
            lblS_UrunAdet.Text = s_urun.Adet.ToString();
            decimal toplamFiyat = s_urun.ToplamFiyat;
            sqlCommand.Parameters.AddWithValue("@id", s_urun.urun.Id);
            sqlCommand.Parameters.AddWithValue("@adet", s_urun.Adet);
            sqlCommand.Parameters.AddWithValue("@tutar", toplamFiyat);
            sqlCommand.ExecuteNonQuery();
            con.Close();

            lblS_UrunTutar.Text = toplamFiyat.ToString("0.00") + " TL";
            ToplamArtti?.Invoke(this, EventArgs.Empty);
        }

        private void btnAzalt_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("UPDATE aski_1 SET adet=@adet, tutar=@tutar WHERE id=@id", con);
            s_urun.Adet--;
            if (s_urun.Adet == 1)
                btnAzalt.Enabled = false;
            if (s_urun.Adet == 0)
                UrunSilindi?.Invoke(this, EventArgs.Empty);
            lblS_UrunAdet.Text = s_urun.Adet.ToString();
            decimal toplamFiyat = s_urun.ToplamFiyat;
            cmd.Parameters.AddWithValue("@id", s_urun.urun.Id);
            cmd.Parameters.AddWithValue("@adet", s_urun.Adet);
            cmd.Parameters.AddWithValue("@tutar", toplamFiyat);
            cmd.ExecuteNonQuery();
            con.Close();
            lblS_UrunTutar.Text = toplamFiyat.ToString("0.00") + " TL";
            ToplamAzaldi?.Invoke(this, EventArgs.Empty);
        }
    }


}
