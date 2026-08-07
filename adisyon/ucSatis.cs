using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using adisyon.Models;
using adisyon.Printers;
using System.Data.SqlClient;
using System.Configuration;

namespace adisyon
{
    
    public partial class ucSatis : UserControl
    {
        public event EventHandler<ucUrunKart> UrunSecildi;
        public SepetUrun s_urun { get; private set; }

        public ucSatis()
        {
            InitializeComponent();
        }

        public static int sepethakki;
        public static bool btnAktif = false;

        void btnvisible(int opt)
        {
            if(opt == 1)
            {
                txtAra.Enabled = false;
                btnTemizle.Enabled = false;
                btnNakit.Enabled = false;
                btnKart.Enabled = false;
                btnSatissizIslem.Enabled = false;
            }
            else
            {
                txtAra.Enabled = true;
                btnTemizle.Enabled = true;
                btnNakit.Enabled = true;
                btnKart.Enabled = true;
                btnSatissizIslem.Enabled = true;
            }
        }
        void StokGuncelle()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            foreach (ucSepettekiUrunler item in flowLayoutPanel1.Controls)
            {
                SqlCommand com = new SqlCommand("update urunler set stok = stok - @adet where barkod = @barkod", con);
                com.Parameters.AddWithValue("@adet", item.s_urun.Adet);
                com.Parameters.AddWithValue("@barkod", item.s_urun.urun.Barkod);
                com.ExecuteNonQuery();
            }
            con.Close();
        }

        void UrunleriYukle(int opt)
        {
            if(opt == 1)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM urunler", con);
                SqlDataReader reader = cmd.ExecuteReader();
                urunAlaniPanel.Controls.Clear();
                while (reader.Read())
                {
                    Urun urun = new Urun();
                    urun.Id = Convert.ToInt32(reader["id"]);
                    urun.Barkod = reader["barkod"].ToString();
                    urun.Ad = reader["ad"].ToString();
                    urun.kategori = reader["kategori"].ToString();
                    urun.Fiyat = Convert.ToDecimal(reader["fiyat"]);
                    urun.Stok = Convert.ToInt32(reader["stok"]);
                    urun.KritikStok = Convert.ToInt32(reader["kritik_stok"]);
                    urun.resimYolu = reader["img"].ToString();
                    ucUrunKart kart = new ucUrunKart();
                    kart.SetUrun(urun);
                    kart.Name = "urunKart" + (urun.Id);
                    kart.UrunSecildi += Kart_UrunSecildi;
                    urunAlaniPanel.Controls.Add(kart);
                }


                con.Close();
            }

            else if(opt == 2)
            {
                foreach(ucSepettekiUrunler item in flowLayoutPanel1.Controls)
                {
                    foreach(ucUrunKart kart in urunAlaniPanel.Controls)
                    {
                        if(item.s_urun.urun.Barkod == kart.urun.Barkod)
                        {
                            kart.urun.Stok -= item.s_urun.Adet;
                            kart.lblStok.Text = "Stok: " + kart.urun.Stok.ToString();
                            break;
                        }
                    }
                }
            }
        }

        static int fisId;
        void AskidaFisVarmi()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand com = new SqlCommand("select count(*) from aski_1", con);
            SqlCommand com2 = new SqlCommand("select count(*) from aski_2", con);
            if ((int)com.ExecuteScalar() > 0 && (int)com2.ExecuteScalar() == 0)
            {
                sepetmodu = false;
                sepethakki = 1;
                btnAktif = false;
                SqlDataReader reader = new SqlCommand("select * from aski_1", con).ExecuteReader();
                while (reader.Read())
                {
                    foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                    {
                        if (kart.urun.Id == Convert.ToInt32(reader["id"]))
                        {
                            ucSepettekiUrunler urun = new ucSepettekiUrunler();
                            urun.SetUrun(kart.urun, Convert.ToInt32(reader["adet"]));
                            urun.Anchor = AnchorStyles.Left;
                            urun.UrunSilindi += Urun_silindi;
                            urun.ToplamArtti += Urun_ToplamArtti;
                            urun.ToplamAzaldi += Urun_ToplamAzaldi;
                            flowLayoutPanel1.Controls.Add(urun);
                            flowLayoutPanel1.ScrollControlIntoView(urun);
                        }
                    }
                }
                fisId = 1;
                label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
                lblToplamTutar.Text = $"TOPLAM : {flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat)} TL";
                reader.Close();
                btnvisible(2);
                sepetmodu = false;
                template.ShowMessageInfo("Askıda bekleyen fiş bulundu. Sepetiniz otomatik olarak yüklendi.", this.FindForm());
                txtAra.Focus();
            }

            else if((int)com.ExecuteScalar() == 0 && (int)com2.ExecuteScalar() > 0)
            {
                sepetmodu = false;
                sepethakki = 1;
                btnAktif = false;
                SqlDataReader reader = new SqlCommand("select * from aski_2", con).ExecuteReader();

                while (reader.Read())
                {
                    foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                    {
                        if (kart.urun.Id == Convert.ToInt32(reader["id"]))
                        {
                            ucSepettekiUrunler urun = new ucSepettekiUrunler();
                            urun.SetUrun(kart.urun, Convert.ToInt32(reader["adet"]));
                            urun.Anchor = AnchorStyles.Left;
                            urun.UrunSilindi += Urun_silindi;
                            urun.ToplamArtti += Urun_ToplamArtti;
                            urun.ToplamAzaldi += Urun_ToplamAzaldi;
                            flowLayoutPanel1.Controls.Add(urun);
                            flowLayoutPanel1.ScrollControlIntoView(urun);
                        }
                    }
                }
                
                fisId = 2;
                label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
                lblToplamTutar.Text = $"TOPLAM : {flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat)} TL";
                reader.Close();
                btnvisible(2);
                sepetmodu = false;
                template.ShowMessageInfo("Askıda bekleyen fiş bulundu. Sepetiniz otomatik olarak yüklendi.", this.FindForm());
                txtAra.Focus();
            }

            

            else if((int)com.ExecuteScalar() > 0 && (int)com2.ExecuteScalar() > 0)
            {
                sepetmodu |= true;
                btnvisible(1);
                sepethakki = 2;
                btnAktif = true;
                for (int i = 0; i < 2; i++)
                {
                    
                    decimal tplm = Convert.ToDecimal(new SqlCommand($"select SUM(tutar) from aski_{i + 1}", con).ExecuteScalar());
                    Guna2TileButton btn = new Guna2TileButton();
                    btn.Name = $"btnFis{i + 1}";
                    btn.Text = $"Fiş {i + 1} - Toplam : {tplm} TL";
                    btn.ForeColor = Color.White;
                    btn.FillColor = Color.FromArgb(0, 123, 255);
                    btn.Font = new Font("Segoe UI", 15, FontStyle.Regular);
                    btn.Size = new Size(flowLayoutPanel1.Width - flowwidth, 100);
                    int fisno = i + 1;
                    btn.Click += (s, e) =>
                    {
                        flowLayoutPanel1.Controls.Clear();
                        sepetmodu = false;
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
                        con2.Open();
                        SqlDataReader reader = new SqlCommand($"select * from aski_{fisno}", con2).ExecuteReader();
                        while (reader.Read())
                        {
                            foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                            {
                                if (kart.urun.Id == Convert.ToInt32(reader["id"]))
                                {
                                    ucSepettekiUrunler urun = new ucSepettekiUrunler();
                                    urun.SetUrun(kart.urun, Convert.ToInt32(reader["adet"]));
                                    urun.Anchor = AnchorStyles.Left;
                                    urun.UrunSilindi += Urun_silindi;
                                    urun.ToplamArtti += Urun_ToplamArtti;
                                    urun.ToplamAzaldi += Urun_ToplamAzaldi;
                                    flowLayoutPanel1.Controls.Add(urun);
                                    flowLayoutPanel1.ScrollControlIntoView(urun);
                                }
                            }
                        }
                        btnvisible(2);
                        label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
                        lblToplamTutar.Text = $"TOPLAM : {flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat)} TL";
                        reader.Close();
                        con2.Close();
                        template.ShowMessageInfo($"Fiş {fisno} yüklendi.", this.FindForm());
                        fisId = fisno;
                        btnAktif = false;
                        txtAra.Focus();

                    };
                    flowLayoutPanel1.Controls.Add(btn);
                }
                sepetmodu = true;
            }
            
            con.Close();
            
        }
        int flowwidth = 7;
        private void ucSatis_Load(object sender, EventArgs e)
        {
            kalan = 0;
            odenen = 0;
            
            txtAra.Focus();
            flowLayoutPanel1.Width = int.Parse(ConfigurationManager.AppSettings["SagPanelWidth"]);
            sepethakki = 0;
            sepetmodu = false;
            fisId = 1;
            lblToplamTutar.Text = "TOPLAM : 0 TL";
            int width = int.Parse(ConfigurationManager.AppSettings["SagPanelWidth"]);
            sagPanel.Size = new Size(width, this.Height);
            sagPanel.Dock = DockStyle.Right;

            txtAra.PlaceholderText = "Barkod veya Ürün Adı Girin...";
            txtAra.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            txtAra.ForeColor = Color.Black;
            UrunleriYukle(1);
            lblkalan.Visible = false;
            BeginInvoke(new Action(() =>
            {
                AskidaFisVarmi();
            }));
            nakitbasildi = false;
            kartbasildi = false;
            nakit = false;
            kart = false;
            txtAra.Focus();
        }

        public void ToplamiArttir()
        {
            decimal toplamTutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);
            lblToplamTutar.Text = $"TOPLAM : {toplamTutar} TL";
        }

        public void ToplamiAzalt()
        {
            decimal toplamTutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);
            lblToplamTutar.Text = $"TOPLAM : {toplamTutar} TL";
        }

        private void Urun_silindi(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"delete from aski_{fisId} where id=@id", con);
            ucSepettekiUrunler urun = sender as ucSepettekiUrunler;
            cmd.Parameters.AddWithValue("@id", urun.s_urun.urun.Id);
            cmd.ExecuteNonQuery();
            con.Close();
            flowLayoutPanel1.Controls.Remove(urun);
            label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
            ToplamiAzalt();
        }

        private void Urun_ToplamArtti(object sender, EventArgs e)
        {
            ToplamiArttir();
        }
        private void Urun_ToplamAzaldi(object sender, EventArgs e)
        {
            ToplamiAzalt();
        }   
        bool sepetmodu;

        private void Kart_UrunSecildi(object sender, Urun urunn)
        {
            if(sepetmodu == true)
            {
                template.ShowMessageInfo("Lütfen bir sepet seçin.", this.FindForm());
                txtAra.Focus();
                return;
            }
            if(sepethakki < 2 && flowLayoutPanel1.Controls.Count == 0)
                sepethakki++;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd;
            foreach (ucSepettekiUrunler item in flowLayoutPanel1.Controls)
            {
                if (item.s_urun.urun.Barkod == urunn.Barkod)
                {
                    
                    item.AdetGuncelle(s_adet);
                    s_adet = 1;
                    item.butonuAc();
                    cmd = new SqlCommand($"update aski_{fisId} set adet=@adet, tutar=@tutar where id=@id", con);
                    cmd.Parameters.AddWithValue("@adet", item.s_urun.Adet);
                    cmd.Parameters.AddWithValue("@tutar", item.s_urun.ToplamFiyat);
                    cmd.Parameters.AddWithValue("@id", item.s_urun.urun.Id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    flowLayoutPanel1.ScrollControlIntoView(item);
                    lblToplamTutar.Text = $"TOPLAM : {flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat)} TL";
                    txtAra.Focus();
                    return;
                }
            }

            ucSepettekiUrunler urun = new ucSepettekiUrunler();
            if(s_adet < 2)
                s_adet = 1;
            int tmp = s_adet;
            urun.SetUrun(urunn, tmp);
            urun.Anchor = AnchorStyles.Left;
            urun.UrunSilindi += Urun_silindi;
            urun.ToplamArtti += Urun_ToplamArtti;
            urun.ToplamAzaldi += Urun_ToplamAzaldi;
            flowLayoutPanel1.Controls.Add(urun);
            flowLayoutPanel1.ScrollControlIntoView(urun);
            cmd = new SqlCommand($"insert into aski_{fisId} (id, ad, adet, fiyat, tutar) values (@id, @ad, @adet, @fiyat, @tutar)", con);
            cmd.Parameters.AddWithValue("@id", urunn.Id);
            cmd.Parameters.AddWithValue("@ad", urunn.Ad);
            cmd.Parameters.AddWithValue("@adet", 1);
            cmd.Parameters.AddWithValue("@fiyat", urunn.Fiyat);
            cmd.Parameters.AddWithValue("@tutar", urun.s_urun.ToplamFiyat);
            cmd.ExecuteNonQuery();
            con.Close();
            label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
           lblToplamTutar.Text = $"TOPLAM : {flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat)} TL";
            txtAra.Focus();

        }

        private bool IsNumeric(string text)
        {
            return decimal.TryParse(text, out _);
        }
        int s_adet;
        string brkd;
        decimal odenen;
        public void kalanGoster(bool yes, bool kartt, decimal _kalan, decimal _odenen)
        {
            nakit = yes;
            kart = kartt;
            kalan = _kalan;

            if (!yes && !kartt)
            {
                odenen = 0;
                lblkalan.Visible = false;
                lblkalan.Text = string.Empty;
                return;
            }
            
                lblkalan.Visible = true;
            odenen += _odenen;
            lblkalan.Text = $"ÖDENEN : {odenen} TL / KALAN : {kalan} TL ";
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
                if(IsNumeric(txtAra.Text))
                {
               
                        foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                        {
                            if (kart.urun.Barkod == txtAra.Text)
                            {
                                Kart_UrunSecildi(kart, kart.urun);
                                txtAra.Clear();
                                return;
                            }
                        }
                        template.ShowMessageInfo("Barkod ile eşleşen ürün bulunamadı: " + txtAra.Text, this.FindForm());
                        txtAra.Clear();
                        return;
                    
                }
                
                else if(txtAra.Text.ToLower().Contains('x') && txtAra.Text.Count(c => char.IsLetter(c)) == 1)
                {
                    s_adet = Convert.ToInt32(txtAra.Text.ToLower().Split('x')[0]);
                    brkd = txtAra.Text.ToLower().Split('x')[1];
                    if(IsNumeric(brkd.ToString()))
                    {
                        foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                        {
                            if (kart.urun.Barkod == brkd.ToString())
                            {
                                Kart_UrunSecildi(kart, kart.urun);
                            s_adet = 1;
                            txtAra.Clear();
                                return;
                            }
                        }
                        template.ShowMessageInfo("Barkod ile eşleşen ürün bulunamadı: " + brkd, this.FindForm());
                        txtAra.Clear();
                        return;
                    }
                    return;
                }
                else
                {
                    foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                    {
                        if(string.IsNullOrEmpty(txtAra.Text))
                            kart.Visible = true;
                        else
                            kart.Visible = kart.urun.Ad.ToLower().Contains(txtAra.Text.ToLower());
                    }
                }
                txtAra.Focus();
        }

        bool nakitbasildi;
        bool kartbasildi;

        private void btnKart_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                sepetmodu = false;
                template.ShowMessageInfo("Sepetiniz boşken işlem yapamazsınız.", this.FindForm());
                return;
            }

            NakitSatis nakitSatis;

            t_tutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);
            if (kalan > 0)
                nakitSatis = new NakitSatis(kalan, this);
            else
                nakitSatis = new NakitSatis(t_tutar, this);

            nakitSatis._nakit = false;

            if (nakitSatis.ShowDialog() == DialogResult.OK)
            {
                kartbasildi = true;
               
                sonKartOdenen += nakitSatis.OdenenTutar;
                if (kart == true)
                {
                    if (kalan > 0)
                        return;
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand("insert into satislar (TARIH, TOPLAM, ODEME_TIPI) values (@TARIH, @TOPLAM, @ODEME_TIPI)", con);
                cmd.Parameters.AddWithValue("@TARIH", DateTime.Now);
                cmd.Parameters.AddWithValue("@TOPLAM", flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat));
                if (nakitbasildi == true && kartbasildi == true)
                    cmd.Parameters.AddWithValue("@ODEME_TIPI", "KARMA");
                else
                    cmd.Parameters.AddWithValue("@ODEME_TIPI", "KART");
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"delete from aski_{fisId}", con);
                cmd.ExecuteNonQuery();

                int sonId = getLastData();

                foreach (ucSepettekiUrunler urun in flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>())
                {
                    cmd = new SqlCommand("insert into satisDetay (satisId, ad, birimFiyat, adet, toplamTutar) values (@satisId, @ad, @birimFiyat, @adet, @toplamTutar)", con);
                    cmd.Parameters.AddWithValue("@satisId", sonId);
                    cmd.Parameters.AddWithValue("@ad", urun.s_urun.urun.Ad);
                    cmd.Parameters.AddWithValue("@birimFiyat", urun.s_urun.urun.Fiyat);
                    cmd.Parameters.AddWithValue("@adet", urun.s_urun.Adet);
                    cmd.Parameters.AddWithValue("@toplamTutar", urun.s_urun.ToplamFiyat);
                    cmd.ExecuteNonQuery();
                    stokHareketi(urun.s_urun.urun.Ad, "Satış", urun.s_urun.Adet, urun.s_urun.urun.Stok, urun.s_urun.urun.Stok - urun.s_urun.Adet, false);
                }

                con.Close();
                StokGuncelle();
                UrunleriYukle(2);

                var urunler = FisUrunleriniAl();
                decimal toplam = urunler.Sum(x => x.toplamFiyat);
                decimal paraUstu = nakitSatis.ParaUstu;

                FisOnizlemeGoster(sonId, urunler, sonNakitOdenen, sonKartOdenen, 0m, paraUstu);

                if (sepethakki > 0)
                {
                    sepethakki--;
                }

                fisId = hangiSepet();
                sepetmodu = false;
                flowLayoutPanel1.Controls.Clear();
                label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
                lblToplamTutar.Text = "TOPLAM : 0 TL";
                IslemBilgileriniSifirla();
            }

            txtAra.Focus();
        }

        public static decimal t_tutar = 0;

        void stokHareketi(string urun, string tip, int miktar, int eski_stok, int yeni_stok, bool artanmi)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into stokHareketleri (tarih, urun_ad, işlem, miktar, eski_stok, yeni_stok) values (@tarih, @urun_ad, @işlem, @miktar, @eski_stok, @yeni_stok)", con);
            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
            cmd.Parameters.AddWithValue("@urun_ad", urun);
            cmd.Parameters.AddWithValue("@işlem", tip);
            if(artanmi == true)
                cmd.Parameters.AddWithValue("@miktar", miktar);
            else
                cmd.Parameters.AddWithValue("@miktar", -miktar);
            cmd.Parameters.AddWithValue("@eski_stok", eski_stok);
            cmd.Parameters.AddWithValue("@yeni_stok", yeni_stok);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private int getLastData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 ID FROM satislar ORDER BY ID DESC", con);
            int lastId = (int)cmd.ExecuteScalar();
            con.Close();
            return lastId;
        }

        public static bool nakit;
        public static bool kart;
        public static decimal kalan;
        private void btnNakit_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                sepetmodu = false;
                template.ShowMessageInfo("Sepetiniz boşken işlem yapamazsınız.", this.FindForm());
                return;
            }

            NakitSatis nakitSatis;

            t_tutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);
            if (kalan > 0)
                nakitSatis = new NakitSatis(kalan, this);
            else
                nakitSatis = new NakitSatis(t_tutar, this);

            nakitSatis._nakit = true;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();

            if (nakitSatis.ShowDialog() == DialogResult.OK)
            {
                nakitbasildi = true;
                sonNakitOdenen += nakitSatis.OdenenTutar;
               

                if (nakit == true)
                {
                    if (kalan > 0)
                        return;
                }

                SqlCommand cmd = new SqlCommand("insert into satislar (TARIH, TOPLAM, ODEME_TIPI) values (@TARIH, @TOPLAM, @ODEME_TIPI)", con);
                cmd.Parameters.AddWithValue("@TARIH", DateTime.Now);
                cmd.Parameters.AddWithValue("@TOPLAM", flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat));
                if (nakitbasildi == true && kartbasildi == true)
                    cmd.Parameters.AddWithValue("@ODEME_TIPI", "KARMA");
                else
                    cmd.Parameters.AddWithValue("@ODEME_TIPI", "NAKİT");
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"delete from aski_{fisId}", con);
                cmd.ExecuteNonQuery();

                int sonId = getLastData();

                foreach (ucSepettekiUrunler item in flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>())
                {
                    cmd = new SqlCommand("insert into satisDetay (satisId, ad, birimFiyat, adet, toplamTutar) values (@satisId, @ad, @birimFiyat, @adet, @toplamTutar)", con);
                    cmd.Parameters.AddWithValue("@satisId", sonId);
                    cmd.Parameters.AddWithValue("@ad", item.s_urun.urun.Ad);
                    cmd.Parameters.AddWithValue("@birimFiyat", item.s_urun.urun.Fiyat);
                    cmd.Parameters.AddWithValue("@adet", item.s_urun.Adet);
                    cmd.Parameters.AddWithValue("@toplamTutar", item.s_urun.ToplamFiyat);
                    stokHareketi(item.s_urun.urun.Ad, "Satış", item.s_urun.Adet, item.s_urun.urun.Stok, item.s_urun.urun.Stok - item.s_urun.Adet, false);
                    cmd.ExecuteNonQuery();
                }

                con.Close();
                StokGuncelle();
                UrunleriYukle(2);

                var urunler = FisUrunleriniAl();
                decimal toplam = urunler.Sum(x => x.toplamFiyat);
                decimal paraUstu = nakitSatis.ParaUstu;

                FisOnizlemeGoster(sonId, urunler, sonNakitOdenen, sonKartOdenen, 0m, paraUstu);

                if (sepethakki > 0)
                {
                    sepethakki--;
                }

                fisId = hangiSepet();
                sepetmodu = false;
                flowLayoutPanel1.Controls.Clear();
                label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
                lblToplamTutar.Text = "TOPLAM : 0 TL";
                IslemBilgileriniSifirla();
                txtAra.Focus();
            }
            txtAra.Focus();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                sepetmodu = false;
                template.ShowMessageInfo("Sepetiniz boşken işlem yapamazsınız.", this.FindForm());
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into satislar (TARIH, TOPLAM, ODEME_TIPI) values (@TARIH, @TOPLAM, @ODEME_TIPI)", con);
            cmd.Parameters.AddWithValue("@TARIH", DateTime.Now);
            cmd.Parameters.AddWithValue("@TOPLAM", flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat));
            cmd.Parameters.AddWithValue("@ODEME_TIPI", "SATIŞSIZ İŞLEM");
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"delete from aski_{fisId}", con);
            cmd.ExecuteNonQuery();
            int sonId = getLastData();
            foreach (ucSepettekiUrunler urun in flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>())
            {
                cmd = new SqlCommand("insert into satissizIslemler (ad, birimFiyat, adet, toplamTutar, satisId) values (@ad, @birimFiyat, @adet, @toplamTutar, @satisId)", con);
                cmd.Parameters.AddWithValue("@satisId", sonId);
                cmd.Parameters.AddWithValue("@ad", urun.s_urun.urun.Ad);
                cmd.Parameters.AddWithValue("@birimFiyat", urun.s_urun.urun.Fiyat);
                cmd.Parameters.AddWithValue("@adet", urun.s_urun.Adet);
                cmd.Parameters.AddWithValue("@toplamTutar", urun.s_urun.ToplamFiyat);
                cmd.ExecuteNonQuery();
                stokHareketi(urun.s_urun.urun.Ad, "Satışsız İşlem", urun.s_urun.Adet, urun.s_urun.urun.Stok + urun.s_urun.Adet, urun.s_urun.urun.Stok, false);
            }
            con.Close();
            StokGuncelle();
            UrunleriYukle(2);
            flowLayoutPanel1.Controls.Clear();
            if (sepethakki > 0)
                sepethakki--;
            fisId = hangiSepet();

            sepetmodu = false;
            label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
            lblToplamTutar.Text = "TOPLAM : 0 TL";
            IslemBilgileriniSifirla();
            txtAra.Focus();
        }


        int hangiSepet()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select count(*) from aski_1", con);
            SqlCommand cmd2 = new SqlCommand("select count(*) from aski_2", con);
           
            if ((int)cmd1.ExecuteScalar() > 0 && (int)cmd2.ExecuteScalar() == 0)
                return (2);
            
            

            return 1;
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {

            if (flowLayoutPanel1.Controls.Count == 0)
            {
                txtAra.Focus();
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"delete from aski_{fisId}", con);
            cmd.ExecuteNonQuery();
            con.Close();
            flowLayoutPanel1.Controls.Clear();
            label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
            lblToplamTutar.Text = "TOPLAM : 0 TL";
            if(sepethakki >= 1)
                sepethakki--;
             fisId = hangiSepet();

            sepetmodu = false;
            IslemBilgileriniSifirla();
            template.ShowMessageInfo("Sepet temizlendi.", this.FindForm());
            txtAra.Focus();
        }

        private void btnYeniSepet_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                template.ShowMessageInfo("Sepetiniz boşken yeni sepet açamazsınız.", this.FindForm());
                return;
            }
            
            if (sepethakki < 2)
            {
                if(flowLayoutPanel1.Controls.Count > 0)
                    flowLayoutPanel1.Controls.Clear();
                sepethakki++;
                fisId = hangiSepet();
                //MessageBox.Show($"fiş : {fisId}");
                btnvisible(2);
                label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
                lblToplamTutar.Text = "TOPLAM : 0 TL";
                sepetmodu = false;
                IslemBilgileriniSifirla();
                txtAra.Focus();


            }
            else
            {
                template.ShowMessageInfo("En fazla 2 adet sepet açabilirsiniz.", this.FindForm());
            }
        }

     

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            sepetmodu = true;
            if(sepethakki == 0)
            {
                //MessageBox.Show(sepethakki.ToString());
                sepetmodu=false;
               template.ShowMessageInfo("Başka sepet bulunamadı.", this.FindForm());
                return;
            }

            btnvisible(1);
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 2; i++)
            {
                int fisno = i + 1;
         
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
                con.Open();
               // MessageBox.Show($"değer : {i + 1}");
                int adet = Convert.ToInt32(new SqlCommand($"select count(*) from aski_{i + 1}", con).ExecuteScalar());
                if(adet == 0)
                    continue;

                decimal tplm = Convert.ToDecimal(new SqlCommand($"select SUM(tutar) from aski_{i + 1}", con).ExecuteScalar());
                Guna2TileButton btn = new Guna2TileButton();
                btn.Name = $"btnFis{i + 1}";
                btn.Text = $"Fiş {i + 1} - Toplam : {tplm} TL";
                btn.ForeColor = Color.White;
                btn.FillColor = Color.FromArgb(0, 123, 255);
                btn.Font = new Font("Segoe UI", 15, FontStyle.Regular);
                btn.Size = new Size(flowLayoutPanel1.Width - flowwidth, 100);
                
                btn.Click += (s, a) =>
                {
                    flowLayoutPanel1.Controls.Clear();
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
                    con2.Open();
                    SqlDataReader reader = new SqlCommand($"select * from aski_{fisno}", con2).ExecuteReader();
                    while (reader.Read())
                    {
                        foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                        {
                            if (kart.urun.Id == Convert.ToInt32(reader["id"]))
                            {
                                ucSepettekiUrunler urun = new ucSepettekiUrunler();
                                urun.SetUrun(kart.urun, Convert.ToInt32(reader["adet"]));
                                urun.Anchor = AnchorStyles.Left;
                                urun.UrunSilindi += Urun_silindi;
                                urun.ToplamArtti += Urun_ToplamArtti;
                                urun.ToplamAzaldi += Urun_ToplamAzaldi;
                                flowLayoutPanel1.Controls.Add(urun);
                                flowLayoutPanel1.ScrollControlIntoView(urun);
                            }
                        }
                    }
                    btnvisible(2);
                    label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
                    lblToplamTutar.Text = $"TOPLAM : {flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat)} TL";
                    reader.Close();
                    con2.Close();
                    sepetmodu = false;
                    template.ShowMessageInfo($"Fiş {fisno} yüklendi.", this.FindForm());
                    fisId = fisno;
                    btnAktif = false;
                    IslemBilgileriniSifirla();
                    txtAra.Focus();
                };
                flowLayoutPanel1.Controls.Add(btn);
            }
            IslemBilgileriniSifirla();
            sepethakki = flowLayoutPanel1.Controls.Count;
            label2.Text = "SEPET (0)";
            lblToplamTutar.Text = "TOPLAM : 0 TL";
        }


        public void flowsizechang(int wid, int heig)
        {
            urunAlaniPanel.Size = new Size(wid, heig);
        }
        private void txtAra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2Button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adisyon.Printers.PrinterTest.PreviewSampleReceipt();
        }

        private string FisOnizlemeMetni(string odemeTipi, decimal odenenTutar, decimal kalanTutar, decimal paraUstu)
        {
            ReceiptBuilder builder = new ReceiptBuilder();

            decimal toplamTutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);

            builder.Title("ADİSYON");
            builder.Line();
            builder.Date(DateTime.Now);
            builder.ReceiptNo(fisId);
            builder.Line();

            foreach (ucSepettekiUrunler item in flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>())
            {
                builder.Left(item.s_urun.urun.Ad);
                builder.LeftRight(
                    string.Format("{0} x {1:0.00}", item.s_urun.Adet, item.s_urun.urun.Fiyat),
                    item.s_urun.ToplamFiyat.ToString("0.00"));
            }

            builder.Total(toplamTutar);

            if (!string.IsNullOrWhiteSpace(odemeTipi))
                builder.Payment(odemeTipi, odenenTutar);

            if (kalanTutar > 0)
                builder.Payment("KALAN", kalanTutar);

            if (paraUstu > 0)
                builder.Payment("PARA ÜSTÜ", paraUstu);

            builder.Footer("TEST FİŞİ");

            return builder.Build();
        }

        private List<ReceiptLine> FisUrunleriniAl()
{
    return flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>()
        .Select(x => new ReceiptLine(x.s_urun.urun.Ad, x.s_urun.Adet, x.s_urun.urun.Fiyat))
        .ToList();
}

private void FisOnizlemeGoster(
    int satisNo,
    List<ReceiptLine> urunler,
    decimal nakitTutar,
    decimal kartTutar,
    decimal kalanTutar,
    decimal paraUstu)
{
    ReceiptBuilder builder = new ReceiptBuilder();

    decimal toplamTutar = urunler.Sum(x => x.toplamFiyat);

    builder.Title("ADİSYON");
    builder.Line();
    builder.Date(DateTime.Now);
    builder.ReceiptNo(satisNo);
    builder.Line();

    foreach (ReceiptLine item in urunler)
    {
        builder.Left(item.ad);
        builder.LeftRight(
            string.Format("{0} x {1:0.00}", item.miktar, item.fiyat),
            item.toplamFiyat.ToString("0.00"));
    }

    builder.Total(toplamTutar);
           
    if (nakitTutar > 0)
        builder.Payment("NAKİT", nakitTutar);

    if (kartTutar > 0)
        builder.Payment("KART", kartTutar);

    if (kalanTutar > 0)
        builder.Payment("KALAN", kalanTutar);

    if (paraUstu > 0)
        builder.Payment("PARA ÜSTÜ", paraUstu);

    builder.Footer("TEST FİŞİ");

    MessageBox.Show(builder.Build(), "Fiş Önizleme");
}
        private decimal sonNakitOdenen = 0m;
        private decimal sonKartOdenen = 0m;
        private void IslemBilgileriniSifirla()
{
    odenen = 0m;
    kalan = 0m;
    sonNakitOdenen = 0m;
    sonKartOdenen = 0m;
    nakit = false;
    kart = false;
    nakitbasildi = false;
    kartbasildi = false;
    lblkalan.Visible = false;
    lblkalan.Text = string.Empty;
}
    }
}

/*
 ürünlerin ismi karta ve sepete göre ayarlanacak
yarı kart yarı nakit sistemi eklenecek
son olarak fiş yazıcısı entegre edilecek
 */