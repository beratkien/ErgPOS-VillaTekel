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
    public partial class ucSatisFis : UserControl
    {
        public ucSatisFis()
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

        void getFis()
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd;
            if (guna2ComboBox1.SelectedIndex == 0)
            {
                cmd = new SqlCommand(@"select * from satislar where TARIH >= @startDate and TARIH < DATEADD(DAY, 1, @endDate)", con);
                cmd.Parameters.AddWithValue("@startDate", guna2DateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@endDate", guna2DateTimePicker2.Value.Date);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        guna2DataGridView1.Rows.Add(
                            dr["ID"],
                            dr["TARIH"],
                            dr["TOPLAM"],
                            dr["ODEME_TIPI"]);
                    }
                }
                else
                {
                    template.ShowMessageInfo("Seçilen tarihler arasında satış bulunamadı.", this.FindForm());
                }
                dr.Close();

            }
            else if(guna2ComboBox1.SelectedIndex > 0 && guna2ComboBox1.SelectedIndex < 4)
            {
                cmd = new SqlCommand(@"select * from satislar where TARIH >= @startDate and TARIH < DATEADD(DAY, 1, @endDate) and ODEME_TIPI=@odemeTipi", con);
                cmd.Parameters.AddWithValue("@startDate", guna2DateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@endDate", guna2DateTimePicker2.Value.Date);
                cmd.Parameters.AddWithValue("@odemeTipi", guna2ComboBox1.SelectedItem.ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        guna2DataGridView1.Rows.Add(
                            dr["ID"],
                            dr["TARIH"],
                            dr["TOPLAM"],
                            dr["ODEME_TIPI"]);
                    }
                }
                else
                {
                    template.ShowMessageInfo($"Seçilen tarihler arasında ve {guna2ComboBox1.SelectedItem.ToString()} ödeme tipi için satış bulunamadı.", this.FindForm());
                }
                dr.Close();
            }
            
            
            con.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                template.ShowMessageInfo("Başlangıç tarihi bitiş tarihinden büyük olamaz.", this.FindForm());
                return;
            }

            getFis();
        }

        private void ucSatisFis_Load(object sender, EventArgs e)
        {
            fontAyarla(guna2DataGridView1);
            fontAyarla(guna2DataGridView2);
            guna2ComboBox1.SelectedIndex = 0;
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2DataGridView2.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd;
            if (guna2DataGridView1.CurrentRow.Cells[3].Value.ToString() == "SATIŞSIZ İŞLEM")
            {
                cmd = new SqlCommand(@"select * from satissizIslemler where satisId=@satisId", con);
                cmd.Parameters.AddWithValue("@satisId", guna2DataGridView1.CurrentRow.Cells[0].Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    guna2DataGridView2.Rows.Add(
                        dr["id"],
                        dr["satisId"],
                        dr["ad"],
                        dr["birimFiyat"],
                        dr["adet"],
                        dr["toplamTutar"]);
                }
                dr.Close();
            }
            else
            {

                cmd = new SqlCommand(@"select * from satisDetay where satisId=@satisId", con);
                cmd.Parameters.AddWithValue("@satisId", guna2DataGridView1.CurrentRow.Cells[0].Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    guna2DataGridView2.Rows.Add(
                        dr["id"],
                        dr["satisId"],
                        dr["ad"],
                        dr["birimFiyat"],
                        dr["adet"],
                        dr["toplamTutar"]);
                }
                dr.Close();
            }
            
            con.Close();
        }
    }
}
