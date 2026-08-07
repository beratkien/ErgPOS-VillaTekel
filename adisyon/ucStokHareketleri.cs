using adisyon.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adisyon
{
    public partial class ucStokHareketleri : UserControl
    {
        public ucStokHareketleri()
        {
            InitializeComponent();
        }

        void fontAyarla(Guna2DataGridView view)
        {
            view.DefaultCellStyle.Font =
     new Font("Segoe UI", 12, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14);
            view.ThemeStyle.RowsStyle.Height = 100;
            view.ColumnHeadersHeight = 70;
            for (int i = 0; i < view.Columns.Count; i++)
            {
                view.Columns[i].Width = 150;
            }
        }

        void tarihVisible(bool visible)
        {
            guna2DateTimePicker1.Visible = visible;
            guna2DateTimePicker2.Visible = visible;
            label2.Visible = visible;
            label3.Visible = visible;
            btnTarih.Visible = visible;
        }

        void txtVisible(bool visible)
        {
            label4.Visible = visible;
            txtAra.Visible = visible;
            btnUrunAd.Visible = visible;
        }

        private void ucStokHareketleri_Load(object sender, EventArgs e)
        {
            txtVisible(false);
            tarihVisible(false);
            guna2ComboBox2.Visible = false;
            guna2ComboBox1.SelectedIndex = 0;
            guna2ComboBox2.SelectedIndex = -1;
            fontAyarla(guna2DataGridView1);
            getir();
            

        }

        private void btnTarih_Click(object sender, EventArgs e)
        {
            if (guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                template.ShowMessageInfo("Başlangıç tarihi bitiş tarihinden büyük olamaz.", this.FindForm());
                return;
            }

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from stokHareketleri where tarih between @tarih1 and @tarih2", connection);
            cmd.Parameters.AddWithValue("@tarih1", guna2DateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@tarih2", guna2DateTimePicker2.Value);
            cmd.ExecuteNonQuery();
        }
        void getir()
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select * from stokHareketleri", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(
                    dr["id"],
                    dr["tarih"],
                    dr["urun_ad"],
                    dr["işlem"],
                    dr["miktar"],
                    dr["eski_stok"],
                    dr["yeni_stok"]);
            }
            dr.Close();
            con.Close();
        }

        private void btnUrunAd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            if (string.IsNullOrWhiteSpace(txtAra.Text))
            {
                getir();
                return;
            }
            SqlCommand cmd;
            guna2DataGridView1.Rows.Clear();
            cmd = new SqlCommand(@"select * from stokHareketleri where urun_ad like '%' + @urunAdi + '%'", con);
            cmd.Parameters.AddWithValue("@urunAdi", txtAra.Text);
            cmd.ExecuteNonQuery();



            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(
                    dr["id"],
                    dr["tarih"],
                    dr["urun_ad"],
                    dr["işlem"],
                    dr["miktar"],
                    dr["eski_stok"],
                    dr["yeni_stok"]);
            }
            dr.Close();
            con.Close();
        }

        /*
         * varsayılan
         TARİHE GÖRE
ÜRÜN ADINA GÖRE
İŞLEM TİPİNE GÖRE
AZALAN STOĞA GÖRE
ARTAN STOĞA GÖRE*
        
        */

        void getirAzalanStok()
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select * from stokHareketleri where miktar < 0", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(
                    dr["id"],
                    dr["tarih"],
                    dr["urun_ad"],
                    dr["işlem"],
                    dr["miktar"],
                    dr["eski_stok"],
                    dr["yeni_stok"]);
            }
            dr.Close();
            con.Close();
        }

        void getirArtanStok()
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select * from stokHareketleri where miktar > 0", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(
                    dr["id"],
                    dr["tarih"],
                    dr["urun_ad"],
                    dr["işlem"],
                    dr["miktar"],
                    dr["eski_stok"],
                    dr["yeni_stok"]);
            }
            dr.Close();
            con.Close();
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == 0)
            {
                tarihVisible(false);
                txtVisible(false);
                guna2ComboBox2.Visible = false;
                getir();
                return;
            }
            if (guna2ComboBox1.SelectedIndex == 1)
            {
                tarihVisible(true);
                txtVisible(false);
                guna2ComboBox2.Visible = false;
            }
            else if (guna2ComboBox1.SelectedIndex == 2)
            {
                tarihVisible(false);
                txtVisible(true);
                guna2ComboBox2.Visible = false;
            }
            else
            {
                tarihVisible(false);
                txtVisible(false);
                if (guna2ComboBox1.SelectedIndex == 3)
                {
                    guna2ComboBox2.Visible = true;
                }
                else if (guna2ComboBox1.SelectedIndex == 4)
                {
                    guna2ComboBox2.Visible = false;
                    getirAzalanStok();

                }

                else if (guna2ComboBox1.SelectedIndex == 5)
                {
                    guna2ComboBox2.Visible = false;
                    getirArtanStok();
                }
            }
        }

        /*Satış
Satışsız İşlem
Düzeltme > Arttırma
Düzeltme > Azaltma
Ürün Silme
Ürün Ekleme*/

        void getirIslemTipi(string islemTipi)
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select * from stokHareketleri where işlem = @islemTipi", con);
            cmd.Parameters.AddWithValue("@islemTipi", islemTipi);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guna2DataGridView1.Rows.Add(
                    dr["id"],
                    dr["tarih"],
                    dr["urun_ad"],
                    dr["işlem"],
                    dr["miktar"],
                    dr["eski_stok"],
                    dr["yeni_stok"]);
            }
            dr.Close();
            con.Close();
        }
        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getirIslemTipi(guna2ComboBox2.SelectedItem.ToString());
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
