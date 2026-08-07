using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using adisyon.Models;
using Guna.UI2.WinForms;

namespace adisyon
{
    public partial class ucUrunler : UserControl
    {
        public ucUrunler()
        {
            InitializeComponent();
        }

        void urunleriGetir()
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            SqlCommand cmd = null;
            con.Open();
            cmd = new SqlCommand("select * from kategoriler", con);
            SqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                if (!cmbkategori.Items.Contains(dr2["ad"].ToString()))
                    cmbkategori.Items.Add(dr2["ad"].ToString());
            }
            dr2.Close();
            cmd = new SqlCommand("SELECT * FROM urunler", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(dr["id"], dr["barkod"], dr["ad"], dr["kategori"], dr["fiyat"], dr["stok"], dr["kritik_stok"]);
            }
            con.Close();
        }

        int fontsize = Convert.ToInt32(ConfigurationManager.AppSettings["fontsize"]);

        
        private void ucUrunler_Load(object sender, EventArgs e)
        {
            guna2ComboBox2.SelectedIndex = 0;
            cmbkategori.SelectedIndex = 0;
            txtid.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txtad.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txtbarkod.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txtfiyat.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txtkritikstok.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txtstok.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txtresim.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);

            sagpanell.Visible = false;
            urunleriGetir();
            guna2DataGridView1.DefaultCellStyle.Font =
    new Font("Segoe UI", 12, FontStyle.Regular);
            guna2ComboBox1.SelectedIndex = 0;
            txtAra.PlaceholderText = "Ürün adı giriniz...";
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 140;
            guna2DataGridView1.ColumnHeadersHeight = 40;
            guna2DataGridView1.Columns[0].Width = 150;
            guna2DataGridView1.Columns[1].Width = 150;
            guna2DataGridView1.Columns[2].Width = 150;

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(guna2ComboBox1.SelectedIndex == 0)
                txtAra.PlaceholderText = "Ürün adı giriniz...";
            else if(guna2ComboBox1.SelectedIndex == 1)
                txtAra.PlaceholderText = "Barkod giriniz...";
            else if(guna2ComboBox1.SelectedIndex == 2)
                txtAra.PlaceholderText = "Kategori giriniz...";
            else if(guna2ComboBox1.SelectedIndex == 3)
                txtAra.PlaceholderText = "Fiyat giriniz...";
            else if(guna2ComboBox1.SelectedIndex == 4)
                txtAra.PlaceholderText = "Stok giriniz...";
            else if(guna2ComboBox1.SelectedIndex == 5)
                txtAra.PlaceholderText = "Kritik stok giriniz...";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            SqlCommand cmd = null;
            con.Open();
            if(string.IsNullOrEmpty(txtAra.Text))
            {
                urunleriGetir();
                return;

            }
            if (guna2ComboBox1.SelectedIndex == 0)
                cmd = new SqlCommand("SELECT * FROM urunler WHERE ad LIKE @param", con);
            else if (guna2ComboBox1.SelectedIndex == 1)
                cmd = new SqlCommand("select * from urunler where barkod LIKE @param", con);
            else if (guna2ComboBox1.SelectedIndex == 2)
                cmd = new SqlCommand("select * from urunler where kategori LIKE @param", con);
            else if (guna2ComboBox1.SelectedIndex == 3)
                cmd = new SqlCommand("select * from urunler where fiyat LIKE @param", con);
            else if (guna2ComboBox1.SelectedIndex == 4)
                cmd = new SqlCommand("select * from urunler where stok LIKE @param", con);
            else if (guna2ComboBox1.SelectedIndex == 5)
                cmd = new SqlCommand("select * from urunler where kritik_stok LIKE @param", con);

            if (cmd != null)
            {
                guna2DataGridView1.Rows.Clear();
                cmd.Parameters.AddWithValue("@param", "%" + txtAra.Text + "%");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    guna2DataGridView1.Rows.Add(dr["id"], dr["barkod"], dr["ad"], dr["kategori"], dr["fiyat"], dr["stok"], dr["kritik_stok"]);
                }
            }
            con.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resimler | *.jpg;*.jpeg;*.png;";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string klasor = ConfigurationManager.AppSettings["path"];
                    if (!Directory.Exists(klasor))
                        Directory.CreateDirectory(klasor);
                    string hedef = ofd.FileName;
                    txtresim.Text = hedef.Split('\\').Last();

                    template.ShowMessageInfo("Resim başarıyla yüklendi.", this.FindForm());
                }
                catch (Exception ex)
                {

                    template.ShowMessageInfo("Resim yüklenirken bir hata oluştu: " + ex.Message, this.FindForm());
                }
                
            }
        }

        void temizle()
        {
            txtid.Clear();
            txtad.Clear();
            txtbarkod.Clear();
            txtfiyat.Clear();       
            txtkritikstok.Clear();
            txtstok.Clear();
            cmbkategori.SelectedIndex = 0;
            txtresim.Clear();
        }

        private int isNumeric(string text, int opt)
        {
            if(opt == 2)
            {
                decimal sayii;
                if (decimal.TryParse(text, out sayii))
                    return 1;
                else
                    return -1;
            }
            long sayi;
            if (long.TryParse(text, out sayi))
                return 1;
            else
                return -1;
        }
        private string hataVarmi(int opt)
        {
            if(opt == 2)
                if(string.IsNullOrEmpty(txtid.Text))
                    return "Ürün id boş olamaz.";

            if (string.IsNullOrEmpty(txtad.Text))
                return "Ürün adı boş olamaz.";
            if (string.IsNullOrEmpty(txtbarkod.Text))
                return "Barkod boş olamaz.";
            if(cmbkategori.SelectedIndex == 0)
                return "Kategori seçilmedi.";
            if (string.IsNullOrEmpty(txtfiyat.Text))
                return "Fiyat boş olamaz.";
            if (string.IsNullOrEmpty(txtstok.Text))
                return "Stok boş olamaz.";
            if (string.IsNullOrEmpty(txtkritikstok.Text))
                return "Kritik stok boş olamaz.";
            if (cmbkategori.SelectedIndex == -1)
                return "Kategori seçilmedi.";
            if(isNumeric(txtbarkod.Text, 1) == -1)
                return "Barkod geçerli bir sayı olmalıdır.";
            if(isNumeric(txtfiyat.Text, 2) == -1)
                return "Fiyat geçerli bir sayı olmalıdır.";
            if(isNumeric(txtstok.Text, 1) == -1)
                return "Stok geçerli bir sayı olmalıdır.";
            if(isNumeric(txtkritikstok.Text, 1) == -1)
                return "Kritik stok geçerli bir sayı olmalıdır.";

            return null;
        }

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

        void kategoriSayisiArttir(string kategori)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update kategoriler set toplamSayi=toplamSayi+1 where ad=@ad", con);
            cmd.Parameters.AddWithValue("@ad", kategori);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            string hata = hataVarmi(1);
            if (hata != null)
            {
                template.ShowMessageInfo(hata, this.FindForm());
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();

            SqlDataReader dr = new SqlCommand("SELECT * FROM urunler WHERE barkod = '" + txtbarkod.Text + "'", con).ExecuteReader();
            if(dr.Read())
            {
                template.ShowMessageInfo($"Bu barkod ({txtbarkod.Text}) zaten mevcut.", this.FindForm());
                con.Close();
                dr.Close();
                return;
            }

            dr.Close();
            SqlCommand cmd = new SqlCommand("INSERT INTO urunler (ad, barkod, kategori, fiyat, stok, kritik_stok, img) VALUES (@ad, @barkod, @kategori, @fiyat, @stok, @kritik_stok, @img)", con);
            cmd.Parameters.AddWithValue("@ad", txtad.Text);
            cmd.Parameters.AddWithValue("@barkod", txtbarkod.Text);
            cmd.Parameters.AddWithValue("@kategori", cmbkategori.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(txtfiyat.Text));
            cmd.Parameters.AddWithValue("@stok", Convert.ToInt32(txtstok.Text));
            cmd.Parameters.AddWithValue("@kritik_stok", Convert.ToInt32(txtkritikstok.Text));
            if(string.IsNullOrEmpty(txtresim.Text))
                cmd.Parameters.AddWithValue("@img", "resimyok.png");
            else
                cmd.Parameters.AddWithValue("@img", txtresim.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            stokHareketi(txtad.Text, "Ürün Ekleme", Convert.ToInt32(txtstok.Text), 0, Convert.ToInt32(txtstok.Text), true);
            kategoriSayisiArttir(cmbkategori.SelectedItem.ToString());
            template.ShowMessageInfo("Ürün başarıyla eklendi.", this.FindForm());
            urunleriGetir();
            temizle();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            string hata = hataVarmi(2);
            if (hata != null)
            {
                template.ShowMessageInfo(hata, this.FindForm());
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update urunler set ad=@ad, barkod=@barkod, kategori=@kategori, fiyat=@fiyat, stok=@stok, kritik_stok=@kritik_stok, img=@img where id=@id", con);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.Parameters.AddWithValue("@ad", txtad.Text);
            cmd.Parameters.AddWithValue("@barkod", txtbarkod.Text);
            cmd.Parameters.AddWithValue("@kategori", cmbkategori.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(txtfiyat.Text));
            if(duzeltme == 0)
                cmd.Parameters.AddWithValue("@stok", Convert.ToInt32(txtstok.Text));
            else if(duzeltme == 1)
                cmd.Parameters.AddWithValue("@stok", (Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) + Convert.ToInt32(txtstok.Text)));
            else if (duzeltme == 2)
                cmd.Parameters.AddWithValue("@stok", (Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - Convert.ToInt32(txtstok.Text)));
            cmd.Parameters.AddWithValue("@kritik_stok", Convert.ToInt32(txtkritikstok.Text));
            if (string.IsNullOrEmpty(txtresim.Text))
                cmd.Parameters.AddWithValue("@img", "resimyok.png");
            else
                cmd.Parameters.AddWithValue("@img", txtresim.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            if(Convert.ToInt32(txtstok.Text) > Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()))
            {
                if(duzeltme == 1)
                    stokHareketi(txtad.Text, "Düzeltme > Arttırma", Convert.ToInt32(txtstok.Text), Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()), Convert.ToInt32(txtstok.Text) + Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()), Convert.ToInt32(txtstok.Text) > Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()));
                else if(duzeltme == 0)
                    stokHareketi(txtad.Text, "Düzeltme > Arttırma", Convert.ToInt32(txtstok.Text), Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()), Convert.ToInt32(txtstok.Text), Convert.ToInt32(txtstok.Text) > Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()));
            }
            else if(Convert.ToInt32(txtstok.Text) < Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()))
            {
                if(duzeltme == 2)
                    stokHareketi(txtad.Text, "Düzeltme > Azaltma", Convert.ToInt32(txtstok.Text), Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()),  Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - Convert.ToInt32(txtstok.Text), Convert.ToInt32(txtstok.Text) > Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()));
                else if(duzeltme == 0)
                    stokHareketi(txtad.Text, "Düzeltme > Azaltma", Convert.ToInt32(txtstok.Text), Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()), Convert.ToInt32(txtstok.Text), Convert.ToInt32(txtstok.Text) > Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()));
            }
            template.ShowMessageInfo("Ürün başarıyla güncellendi.", this.FindForm());
            urunleriGetir();
            temizle();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            sagpanell.Visible = true;
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            
            txtid.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            SqlDataReader dr = new SqlCommand("SELECT * FROM urunler WHERE id = '" + txtid.Text + "'", con).ExecuteReader();
            txtbarkod.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtad.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbkategori.SelectedItem = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtfiyat.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString(); // stok raporları, raporlar ve kategori kaldı
            txtstok.Text = guna2DataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtkritikstok.Text = guna2DataGridView1.CurrentRow.Cells[6].Value.ToString();
            if(dr.Read())
            {
                  txtresim.Text = dr["img"].ToString();

            }
            con.Close();
            dr.Close();
            if(sagpanell.Visible == false)
                sagpanell.Visible = true;

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            sagpanell.Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        public static bool sil;
        private void btnsil_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtid.Text))
                {
                template.ShowMessageInfo("Lütfen silinecek ürünü seçin.", this.FindForm());
                return;
            }
            if(template.ShowMessageQuestion("Ürün kalıcı olarak silinecek, emin misiniz?", this.FindForm()) == DialogResult.Yes)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from urunler where id=@id", con);
                cmd.Parameters.AddWithValue("@id", txtid.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                stokHareketi(txtad.Text, "Ürün Silme", Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()), Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()), 0, false);
                template.ShowMessageInfo("Ürün başarıyla silindi.", this.FindForm());
                urunleriGetir();
                temizle();
            }
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            txtresim.Clear();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int duzeltme;
        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            duzeltme = guna2ComboBox2.SelectedIndex;

        }
    }
}
