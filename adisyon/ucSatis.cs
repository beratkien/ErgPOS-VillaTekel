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

        // Ürünleri RAM'e tek seferde yüklemek yerine parça parça yükle
        private int URUN_SAYISI = int.Parse(ConfigurationManager.AppSettings["YuklenenUrun"]);
        private int urunOffset = 0;
        private bool urunYukleniyor = false;
        private bool dahaUrunVar = true;

        // Arama pagination bilgileri
        private bool aramaModu = false;
        private int aramaOffset = 0;
        private bool aramaYukleniyor = false;
        private bool aramadaDahaUrunVar = true;
        private string aktifArama = string.Empty;

        public ucSatis()
        {
            InitializeComponent();

            // Ürün panelinin scrollbar'ını dinle
            urunAlaniPanel.Scroll += UrunAlaniPanel_Scroll;
        }

        public static int sepethakki;
        public static bool btnAktif = false;

        void btnvisible(int opt)
        {
            if (opt == 1)
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

        private ucUrunKart UrunKartiOlustur(Urun urun)
        {
            ucUrunKart kart = new ucUrunKart();

            kart.SetUrun(urun);
            kart.Name = "urunKart" + urun.Id;
            kart.UrunSecildi += Kart_UrunSecildi;

            return kart;
        }

        private Urun UrunOku(SqlDataReader reader)
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

            return urun;
        }

        void UrunleriYukle(int opt)
        {
            if (opt == 1)
            {
                if (urunYukleniyor || !dahaUrunVar || aramaModu)
                    return;

                urunYukleniyor = true;

                try
                {
                    using (SqlConnection con = new SqlConnection(
                        ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString))
                    {
                        con.Open();

                        string sql = @"
                        SELECT
                            id,
                            barkod,
                            ad,
                            kategori,
                            fiyat,
                            stok,
                            kritik_stok,
                            img
                        FROM urunler
                        ORDER BY id
                        OFFSET @offset ROWS
                        FETCH NEXT @limit ROWS ONLY";

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@offset", urunOffset);
                            cmd.Parameters.AddWithValue("@limit", URUN_SAYISI);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                int gelenUrunSayisi = 0;

                                while (reader.Read())
                                {
                                    Urun urun = UrunOku(reader);
                                    ucUrunKart kart = UrunKartiOlustur(urun);
                                    urunAlaniPanel.Controls.Add(kart);
                                    gelenUrunSayisi++;
                                }

                                urunOffset += gelenUrunSayisi;

                                if (gelenUrunSayisi < URUN_SAYISI)
                                    dahaUrunVar = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    template.ShowMessageInfo(
                        "Ürünler yüklenirken hata oluştu: " + ex.Message,
                        this.FindForm());
                }
                finally
                {
                    urunYukleniyor = false;
                }
            }
            else if (opt == 2)
            {
                foreach (ucSepettekiUrunler item in flowLayoutPanel1.Controls)
                {
                    foreach (ucUrunKart kart in urunAlaniPanel.Controls)
                    {
                        if (item.s_urun.urun.Barkod == kart.urun.Barkod)
                        {
                            kart.urun.Stok -= item.s_urun.Adet;
                            kart.lblStok.Text = "Stok: " + kart.urun.Stok.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void UrunAlaniPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation != ScrollOrientation.VerticalScroll)
                return;

            int konum = urunAlaniPanel.VerticalScroll.Value;
            int gorunenAlan = urunAlaniPanel.ClientSize.Height;
            int maksimum = urunAlaniPanel.VerticalScroll.Maximum;

            if (konum + gorunenAlan < maksimum - 200)
                return;

            // Arama yapılıyorsa arama sonuçlarının sonraki sayfasını getir
            if (aramaModu)
            {
                UrunAraSayfaYukle();
                return;
            }

            // Normal ürün listesinin sonraki sayfasını getir
            UrunleriYukle(1);
        }

        private void UrunleriSifirla()
        {
            aramaModu = false;
            aktifArama = string.Empty;
            aramaOffset = 0;
            aramaYukleniyor = false;
            aramadaDahaUrunVar = true;

            urunOffset = 0;
            dahaUrunVar = true;
            urunYukleniyor = false;

            urunAlaniPanel.Controls.Clear();
            UrunleriYukle(1);
        }

        private void UrunAra(string arama)
        {
            if (string.IsNullOrWhiteSpace(arama))
            {
                UrunleriSifirla();
                return;
            }

            // Yeni arama başladıysa arama pagination'ını sıfırla
            if (!aramaModu || !string.Equals(aktifArama, arama, StringComparison.OrdinalIgnoreCase))
            {
                aramaOffset = 0;
                aramadaDahaUrunVar = true;
                aktifArama = arama;
                aramaModu = true;
                aramaYukleniyor = false;

                urunAlaniPanel.Controls.Clear();
            }

            UrunAraSayfaYukle();
        }

        private void UrunAraSayfaYukle()
        {
            if (!aramaModu ||
                aramaYukleniyor ||
                !aramadaDahaUrunVar ||
                string.IsNullOrWhiteSpace(aktifArama))
                return;

            aramaYukleniyor = true;

            try
            {
                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString))
                {
                    con.Open();

                    string sql = @"
                        SELECT
                            id,
                            barkod,
                            ad,
                            kategori,
                            fiyat,
                            stok,
                            kritik_stok,
                            img
                        FROM urunler
                        WHERE ad LIKE @arama
                           OR barkod LIKE @arama
                        ORDER BY id
                        OFFSET @offset ROWS
                        FETCH NEXT @limit ROWS ONLY";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@arama", "%" + aktifArama + "%");
                        cmd.Parameters.AddWithValue("@offset", aramaOffset);
                        cmd.Parameters.AddWithValue("@limit", URUN_SAYISI);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int gelenUrunSayisi = 0;

                            while (reader.Read())
                            {
                                Urun urun = UrunOku(reader);
                                ucUrunKart kart = UrunKartiOlustur(urun);

                                urunAlaniPanel.Controls.Add(kart);
                                gelenUrunSayisi++;
                            }

                            aramaOffset += gelenUrunSayisi;

                            if (gelenUrunSayisi < URUN_SAYISI)
                                aramadaDahaUrunVar = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                template.ShowMessageInfo(
                    "Ürün aranırken hata oluştu: " + ex.Message,
                    this.FindForm());
            }
            finally
            {
                aramaYukleniyor = false;
            }
        }

        private Urun BarkoddanUrunGetir(string barkod)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString))
            {
                con.Open();

                string sql = @"
                SELECT
                    id,
                    barkod,
                    ad,
                    kategori,
                    fiyat,
                    stok,
                    kritik_stok,
                    img
                FROM urunler
                WHERE barkod = @barkod";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@barkod", barkod);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return UrunOku(reader);
                    }
                }
            }

            return null;
        }

        static int fisId;

        private List<SepetUrun> AskiUrunleriniGetir(int fisno)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            List<SepetUrun> urunler = new List<SepetUrun>();

            string query = $@"
                SELECT
                    u.id,
                    u.barkod,
                    u.ad,
                    u.kategori,
                    u.fiyat,
                    u.stok,
                    u.kritik_stok,
                    u.img,
                    a.adet
                FROM aski_{fisno} a
                INNER JOIN urunler u
                    ON a.id = u.id
                ORDER BY u.id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Urun urun = UrunOku(reader);

                    urunler.Add(new SepetUrun
                    {
                        urun = urun,
                        Adet = Convert.ToInt32(reader["adet"])
                    });
                }
            }
            con.Close();

            return urunler;
        }

        private void AskiyiSepeteYukle(List<SepetUrun> urunler)
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (SepetUrun sepetUrun in urunler)
            {
                ucSepettekiUrunler urun = new ucSepettekiUrunler();

                urun.SetUrun(sepetUrun.urun, sepetUrun.Adet);
                urun.Anchor = AnchorStyles.Left;
                urun.UrunSilindi += Urun_silindi;
                urun.ToplamArtti += Urun_ToplamArtti;
                urun.ToplamAzaldi += Urun_ToplamAzaldi;

                flowLayoutPanel1.Controls.Add(urun);
            }

            label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
            lblToplamTutar.Text =
                $"TOPLAM : {flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat)} TL";

            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.ScrollControlIntoView(
                    flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]);
            }
        }

        void AskidaFisVarmi()
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString))
            {
                con.Open();

                int aski1Count = Convert.ToInt32(
                    new SqlCommand("SELECT COUNT(*) FROM aski_1", con).ExecuteScalar());

                int aski2Count = Convert.ToInt32(
                    new SqlCommand("SELECT COUNT(*) FROM aski_2", con).ExecuteScalar());

                if (aski1Count > 0 && aski2Count == 0)
                {
                    sepetmodu = false;
                    sepethakki = 1;
                    btnAktif = false;

                    AskiyiSepeteYukle(AskiUrunleriniGetir(1));

                    fisId = 1;
                    btnvisible(2);
                    sepetmodu = false;

                    template.ShowMessageInfo(
                        "Askıda bekleyen fiş bulundu. Sepetiniz otomatik olarak yüklendi.",
                        this.FindForm());

                    TxtAraOdakla();
                }
                else if (aski1Count == 0 && aski2Count > 0)
                {
                    sepetmodu = false;
                    sepethakki = 1;
                    btnAktif = false;

                    AskiyiSepeteYukle(AskiUrunleriniGetir(2));

                    fisId = 2;
                    btnvisible(2);
                    sepetmodu = false;

                    template.ShowMessageInfo(
                        "Askıda bekleyen fiş bulundu. Sepetiniz otomatik olarak yüklendi.",
                        this.FindForm());

                    TxtAraOdakla();
                }
                else if (aski1Count > 0 && aski2Count > 0)
                {
                    sepetmodu = true;
                    btnvisible(1);
                    sepethakki = 2;
                    btnAktif = true;

                    for (int i = 1; i <= 2; i++)
                    {
                        int fisno = i;

                        decimal tplm = Convert.ToDecimal(
                            new SqlCommand(
                                $"SELECT COALESCE(SUM(tutar), 0) FROM aski_{fisno}",
                                con).ExecuteScalar());

                        Guna2TileButton btn = new Guna2TileButton();

                        btn.Name = $"btnFis{fisno}";
                        btn.Text = $"Fiş {fisno} - Toplam : {tplm} TL";
                        btn.ForeColor = Color.White;
                        btn.FillColor = Color.FromArgb(0, 123, 255);
                        btn.Font = new Font("Segoe UI", 15, FontStyle.Regular);
                        btn.Size = new Size(flowLayoutPanel1.Width - flowwidth, 100);

                        btn.Click += (s, e) =>
                        {
                            sepetmodu = false;

                            AskiyiSepeteYukle(
                                AskiUrunleriniGetir(fisno));

                            btnvisible(2);

                            fisId = fisno;
                            btnAktif = false;

                            template.ShowMessageInfo(
                                $"Fiş {fisno} yüklendi.",
                                this.FindForm());

                            txtAra.Focus();
                        };

                        flowLayoutPanel1.Controls.Add(btn);
                    }

                    sepetmodu = true;
                }

            }
            TxtAraOdakla();
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
            UrunleriSifirla();
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
            TxtAraOdakla();
        }

        public void ToplamiAzalt()
        {
            decimal toplamTutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);
            lblToplamTutar.Text = $"TOPLAM : {toplamTutar} TL";
            TxtAraOdakla();
        }

        private void Urun_silindi(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"delete from aski_{fisId} where id=@id", con);
            ucSepettekiUrunler urun = sender as ucSepettekiUrunler;
            cmd.Parameters.AddWithValue("@id", urun.s_urun.urun.Id);
            cmd.ExecuteNonQuery();
            if (flowLayoutPanel1.Controls.Count == 1)
            {
                SqlCommand cmd2 = new SqlCommand($"delete from aski_{fisId} where id=@id", con);
                cmd2.Parameters.AddWithValue("@id", urun.s_urun.urun.Id);
                cmd2.ExecuteNonQuery();
                fisId = hangiSepet();
                sepethakki--;
            }
            con.Close();
            flowLayoutPanel1.Controls.Remove(urun);
            label2.Text = $"SEPET ({flowLayoutPanel1.Controls.Count})";
            ToplamiAzalt();
            TxtAraOdakla();
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
            if (sepetmodu == true)
            {
                template.ShowMessageInfo("Lütfen bir sepet seçin.", this.FindForm());
                txtAra.Focus();
                return;
            }
            if (sepethakki < 2 && flowLayoutPanel1.Controls.Count == 0)
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
            if (s_adet < 2)
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
            lblkalan.Text = $"ÖDENEN : {odenen.ToString("0.00")} TL / KALAN : {kalan.ToString("0.00")} TL ";
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string aramaMetni = txtAra.Text.Trim();

            // Sayısal giriş = barkod
            if (IsNumeric(aramaMetni))
            {
                Urun urun = BarkoddanUrunGetir(aramaMetni);

                if (urun != null)
                {
                    Kart_UrunSecildi(this, urun);
                    txtAra.Clear();
                    txtAra.Focus();
                    return;
                }

                template.ShowMessageInfo(
                    "Barkod ile eşleşen ürün bulunamadı: " + aramaMetni,
                    this.FindForm());

                txtAra.Focus();
                txtAra.Clear();
                return;
            }

            // "3x123456" şeklinde adet + barkod
            if (aramaMetni.ToLower().Contains('x') &&
                aramaMetni.Count(c => char.IsLetter(c)) == 1)
            {
                string[] parcalar = aramaMetni.ToLower().Split('x');

                if (parcalar.Length == 2 &&
                    int.TryParse(parcalar[0], out int adet))
                {
                    s_adet = adet;
                    string barkod = parcalar[1];

                    if (IsNumeric(barkod))
                    {
                        Urun urun = BarkoddanUrunGetir(barkod);

                        if (urun != null)
                        {
                            Kart_UrunSecildi(this, urun);
                            s_adet = 1;
                            txtAra.Clear();
                            txtAra.Focus();
                            return;
                        }

                        template.ShowMessageInfo(
                            "Barkod ile eşleşen ürün bulunamadı: " + barkod,
                            this.FindForm());

                        s_adet = 1;
                        txtAra.Focus();
                        txtAra.Clear();
                        return;
                    }
                }

                s_adet = 1;
                txtAra.Focus();
                return;
            }

            // Metin girişi = SQL üzerinden ürün arama
            UrunAra(aramaMetni);
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
                TxtAraOdakla();
                return;
            }

            NakitSatis nakitSatis;

            t_tutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);
            if (kalan > 0)
                nakitSatis = new NakitSatis(kalan, this, "KART");
            else
                nakitSatis = new NakitSatis(t_tutar, this, "KART");

            nakitSatis._nakit = false;

            if (nakitSatis.ShowDialog() == DialogResult.OK)
            {
                kartbasildi = true;

                sonKartOdenen += nakitSatis.OdenenTutar;
                if (kart == true)
                {
                    if (kalan > 0)
                    {
                        TxtAraOdakla();
                        return;
                    }
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

                // FisOnizlemeGoster(sonId, urunler, sonNakitOdenen, sonKartOdenen, 0m, paraUstu);
                FisYazdir(sonId, sonNakitOdenen, sonKartOdenen, kalan, nakitSatis.ParaUstu);

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
                TxtAraOdakla();
            }

            TxtAraOdakla();
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
            if (artanmi == true)
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
                TxtAraOdakla();
                return;
            }

            NakitSatis nakitSatis;

            t_tutar = flowLayoutPanel1.Controls.Cast<ucSepettekiUrunler>().Sum(x => x.s_urun.ToplamFiyat);
            if (kalan > 0)
                nakitSatis = new NakitSatis(kalan, this, "NAKİT");
            else
                nakitSatis = new NakitSatis(t_tutar, this, "NAKİT");

            nakitSatis._nakit = true;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();

            if (nakitSatis.ShowDialog() == DialogResult.OK)
            {
                nakitbasildi = true;
                sonNakitOdenen += nakitSatis.OdenenTutar;
                nakitSatis.lblTip.Text += "\nNAKİT";

                if (nakit == true)
                {
                    if (kalan > 0)
                    {
                        TxtAraOdakla();
                        return;
                    }
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

                //FisOnizlemeGoster(sonId, urunler, sonNakitOdenen, sonKartOdenen, 0m, paraUstu);
                FisYazdir(sonId, sonNakitOdenen, sonKartOdenen, kalan, nakitSatis.ParaUstu);

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
                TxtAraOdakla();
            }
            TxtAraOdakla();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                sepetmodu = false;
                template.ShowMessageInfo("Sepetiniz boşken işlem yapamazsınız.", this.FindForm());
                TxtAraOdakla();
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
            TxtAraOdakla();
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
                TxtAraOdakla();
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
            if (sepethakki >= 1)
                sepethakki--;
            fisId = hangiSepet();

            sepetmodu = false;
            IslemBilgileriniSifirla();
            template.ShowMessageInfo("Sepet temizlendi.", this.FindForm());
            TxtAraOdakla();
        }

        private void btnYeniSepet_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                template.ShowMessageInfo("Sepetiniz boşken yeni sepet açamazsınız.", this.FindForm());
                TxtAraOdakla();
                return;
            }

            if (sepethakki < 2)
            {
                if (flowLayoutPanel1.Controls.Count > 0)
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
            if (sepethakki == 0)
            {
                //MessageBox.Show(sepethakki.ToString());
                sepetmodu = false;
                template.ShowMessageInfo("Başka sepet bulunamadı.", this.FindForm());
                TxtAraOdakla();
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
                if (adet == 0)
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
                    sepetmodu = false;

                    AskiyiSepeteYukle(
                        AskiUrunleriniGetir(fisno));

                    btnvisible(2);

                    fisId = fisno;
                    btnAktif = false;

                    template.ShowMessageInfo(
                        $"Fiş {fisno} yüklendi.",
                        this.FindForm());

                    TxtAraOdakla();
                    /*flowLayoutPanel1.Controls.Clear();
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
                    txtAra.Focus();*/
                };
                flowLayoutPanel1.Controls.Add(btn);
            }
            IslemBilgileriniSifirla();
            sepethakki = flowLayoutPanel1.Controls.Count;
            label2.Text = "SEPET (0)";
            lblToplamTutar.Text = "TOPLAM : 0 TL";
            TxtAraOdakla();
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
        private void FisYazdir(int satisNo, decimal nakitTutar, decimal kartTutar, decimal kalanTutar, decimal paraUstu)
        {
            if (!ReceiptPrinter.HasSelectedPrinter())
            {
                template.ShowMessageInfo("Önce fiş yazıcısı seçilmelidir.", this.FindForm());
                return;
            }

            List<ReceiptLine> urunler = FisUrunleriniAl();

            try
            {
                PrinterManager printerManager = new PrinterManager();
                printerManager.PrintSaleReceipt(
                    satisNo,
                    DateTime.Now,
                    "ADİSYON",
                    string.Empty,
                    urunler,
                    nakitTutar,
                    kartTutar,
                    kalanTutar,
                    paraUstu,
                    string.Empty);
            }
            catch (Exception ex)
            {
                template.ShowMessageInfo("Fiş yazdırılamadı: " + ex.Message, this.FindForm());
            }
        }

        private void TxtAraOdakla()
        {
            BeginInvoke(new Action(() =>
            {
                txtAra.Focus();
                txtAra.SelectAll();
            }));
        }
    }
}