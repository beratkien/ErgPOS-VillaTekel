using Guna.UI2.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using adisyon.Models;


namespace adisyon
{
    public partial class ucRaporlar : UserControl
    {
        public ucRaporlar()
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

        void gunlukRapor()
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(@"
SELECT
    SUM(TOPLAM) AS ToplamTutar,
    SUM(CASE WHEN ODEME_TIPI='NAKİT' THEN TOPLAM ELSE 0 END) AS Nakit,
    SUM(CASE WHEN ODEME_TIPI='KART' THEN TOPLAM ELSE 0 END) AS Kart,
    COUNT(*) AS SatisSayisi,
    SUM(CASE WHEN ODEME_TIPI='SATIŞSIZ İŞLEM' THEN 1 ELSE 0 END) AS SatissizIslem
FROM satislar
WHERE CAST(TARIH AS DATE)=CAST(GETDATE() AS DATE)", con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                guna2DataGridView1.Rows.Add(
                    dr["ToplamTutar"],
                    dr["Nakit"],
                    dr["Kart"],
                    dr["SatisSayisi"],
                    dr["SatissizIslem"]);
            }

            dr.Close();
            con.Close();
        }

        void toplamRapor()
        {
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(@"
SELECT
    SUM(TOPLAM) AS ToplamTutar,
    SUM(CASE WHEN ODEME_TIPI='NAKİT' THEN TOPLAM ELSE 0 END) AS Nakit,
    SUM(CASE WHEN ODEME_TIPI='KART' THEN TOPLAM ELSE 0 END) AS Kart,
    COUNT(*) AS SatisSayisi,
    SUM(CASE WHEN ODEME_TIPI='SATIŞSIZ İŞLEM' THEN 1 ELSE 0 END) AS SatissizIslem
FROM satislar ", con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                guna2DataGridView1.Rows.Add(
                    dr["ToplamTutar"],
                    dr["Nakit"],
                    dr["Kart"],
                    dr["SatisSayisi"],
                    dr["SatissizIslem"]);
            }

            dr.Close();
            con.Close();
        }

        void tarihBazlıRapor()
        {
            if(guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                template.ShowMessageInfo("Başlangıç tarihi bitiş tarihinden büyük olamaz.", this.FindForm());
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"
                    SELECT
                    SUM(TOPLAM) AS ToplamTutar,
                    SUM(CASE WHEN ODEME_TIPI='NAKİT' THEN TOPLAM ELSE 0 END) AS Nakit,
                    SUM(CASE WHEN ODEME_TIPI='KART' THEN TOPLAM ELSE 0 END) AS Kart,
                    COUNT(*) AS SatisSayisi,
                    SUM(CASE WHEN ODEME_TIPI='SATIŞSIZ İŞLEM' THEN 1 ELSE 0 END) AS SatissizIslem
                    FROM satislar
                    WHERE TARIH >= @basla AND TARIH <= DATEADD(DAY, 1, @bitis)", con);
            cmd.Parameters.AddWithValue("@basla", guna2DateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@bitis", guna2DateTimePicker2.Value.Date);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                guna2DataGridView1.Rows.Clear();
                guna2DataGridView1.Rows.Add(
                    dr["ToplamTutar"],
                    dr["Nakit"],
                    dr["Kart"],
                    dr["SatisSayisi"],
                    dr["SatissizIslem"]);
            }
        }

        void kritikStokRapor()
        {
            //tablo en baştan oluşacak
            guna2DataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["adisyon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select barkod, ad, stok, kritik_stok from urunler where stok <= kritik_stok + 5", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                guna2DataGridView2.Rows.Add(
                    dr["barkod"],
                    dr["ad"],
                    dr["stok"],
                    dr["kritik_stok"]);
            }
            con.Close();
            dr.Close();
        }

        void gizle(bool visible)
        {
            label2.Visible = visible;
            label3.Visible = visible;
            guna2DateTimePicker1.Visible = visible;
            guna2DateTimePicker2.Visible = visible;
            guna2Button1.Visible = visible;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucRaporlar_Load(object sender, EventArgs e)
        {
            fontAyarla(guna2DataGridView1);
            fontAyarla(guna2DataGridView2);
            //fontAyarla(guna2DataGridView2);
            //fontAyarla(guna2DataGridView3);
            guna2ComboBox1.SelectedIndex = 0;
            guna2DataGridView1.Visible = true;
            guna2DataGridView2.Visible = false;
            guna2DataGridView1.Dock = DockStyle.Fill;
            guna2DataGridView2.Dock = DockStyle.None;
            gunlukRapor();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(guna2ComboBox1.SelectedIndex == 0)
            {
                guna2DataGridView1.Visible = true;
                guna2DataGridView1.Dock = DockStyle.Fill;
                guna2DataGridView2.Dock = DockStyle.None;
                guna2DataGridView2.Visible = false;
                gizle(false);
                gunlukRapor();
            }
            else if(guna2ComboBox1.SelectedIndex == 2)
            {
                guna2DataGridView1.Visible = true;
                guna2DataGridView1.Dock = DockStyle.Fill;
                guna2DataGridView2.Dock = DockStyle.None;
                guna2DataGridView2.Visible = false;
                gizle(false);
                toplamRapor();
            }
            else if(guna2ComboBox1.SelectedIndex == 1)
            {
                guna2DataGridView1.Visible = true;
                guna2DataGridView2.Visible = false;
                guna2DataGridView1.Dock = DockStyle.Fill;
                guna2DataGridView2.Dock = DockStyle.None;
                gizle(true);

                
            }

            else if(guna2ComboBox1.SelectedIndex == 3)
            {
                gizle(false);
                guna2DataGridView1.Visible = false;
                guna2DataGridView2.Visible = true;
                guna2DataGridView1.Dock = DockStyle.None;
                guna2DataGridView2.Dock = DockStyle.Fill;
                kritikStokRapor();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            tarihBazlıRapor();

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
