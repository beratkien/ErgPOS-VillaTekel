using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using adisyon.Models;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace adisyon
{
    public partial class ucKategoriler : UserControl
    {
        public ucKategoriler()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (sagpanell.Visible == false)
                sagpanell.Visible = true;
            
        }

        private void sagpanell_Paint(object sender, PaintEventArgs e)
        {

        }
        int fontsize = int.Parse(ConfigurationManager.AppSettings["fontSize"]); 
        private void ucKategoriler_Load(object sender, EventArgs e)
        {
            sagpanell.Visible = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from kategoriler", con);
            SqlDataReader dr = cmd.ExecuteReader();
            guna2DataGridView1.Rows.Clear();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(dr["id"], dr["ad"], dr["toplamSayi"]);
            }
            con.Close();

            dr.Close();
            txtid.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txtad.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            txturunsayisi.Font = new Font("Segoe UI", fontsize, FontStyle.Regular);
            guna2DataGridView1.DefaultCellStyle.Font =
   new Font("Segoe UI", 12, FontStyle.Regular);
            txtAra.PlaceholderText = "Kategori adı giriniz...";
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 140;
            guna2DataGridView1.ColumnHeadersHeight = 50;
            guna2DataGridView1.Columns[0].Width = 150;
            guna2DataGridView1.Columns[1].Width = 150;
            guna2DataGridView1.Columns[2].Width = 150;
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if(sagpanell.Visible == true)
                    sagpanell.Visible = false;
        }

        void urunleriGetir()
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            SqlCommand cmd = null;
            con.Open();
            cmd = new SqlCommand("select * from kategoriler where ad LIKE @ad", con);
            cmd.Parameters.AddWithValue("@ad", "%" + txtAra.Text + "%");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(dr["id"], dr["ad"], dr["toplamSayi"]);
            }
            con.Close();
        }
        void urunleriGetir2()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from kategoriler", con);
            SqlDataReader dr = cmd.ExecuteReader();
            guna2DataGridView1.Rows.Clear();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(dr["id"], dr["ad"], dr["toplamSayi"]);
            }
            con.Close();

            dr.Close();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtAra.Text))
            {
                urunleriGetir2();
                return;
            }
            urunleriGetir();


        }

        void temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txturunsayisi.Text = "";
        }

        bool aynımi(string ad)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from kategoriler where ad = @ad", con);
            cmd.Parameters.AddWithValue("@ad", ad);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Close();
                return true;
            }

            con.Close();
            return false;
            
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtad.Text))
            {
                template.ShowMessageInfo("Kategori adı boş olamaz!", this.FindForm());
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            if(aynımi(txtad.Text))
            {
                template.ShowMessageInfo("Bu kategori zaten mevcut!", this.FindForm());
                con.Close();
                return;
            }
            SqlCommand com = new SqlCommand("insert into kategoriler (ad, toplamSayi) values (@ad, @toplamSayi)", con);
            com.Parameters.AddWithValue("@ad", txtad.Text);
            com.Parameters.AddWithValue("@toplamSayi", 0);
            com.ExecuteNonQuery();
            con.Close();
            urunleriGetir2();
            template.ShowMessageInfo("Kategori başarıyla eklendi.", this.FindForm());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtid.Text))
            {
                template.ShowMessageInfo("Lütfen güncellenecek kategoriyi seçin.", this.FindForm());
                return;
            }
            if(string.IsNullOrEmpty(txtad.Text))
            {
                template.ShowMessageInfo("Kategori adı boş olamaz!", this.FindForm());
                return;
            }
            if(aynımi(txtad.Text))
            {
                template.ShowMessageInfo("Bu kategori zaten mevcut!", this.FindForm());
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update kategoriler set ad = @ad where id = @id", con);
            cmd.Parameters.AddWithValue("@ad", txtad.Text);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            urunleriGetir2();
            template.ShowMessageInfo("Kategori başarıyla güncellendi.", this.FindForm());
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                template.ShowMessageInfo("Lütfen güncellenecek kategoriyi seçin.", this.FindForm());
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from kategoriler where id = @id", con);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            urunleriGetir2();
            template.ShowMessageInfo("Kategori başarıyla silindi.", this.FindForm());

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sagpanell.Visible = true;
            txtid.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtad.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            txturunsayisi.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
