using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace adisyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        int width = int.Parse(ConfigurationManager.AppSettings["solPanelWidth"]);
        int height = int.Parse(ConfigurationManager.AppSettings["ustPanelHeight"]);
        private ucSatis _ucsatis;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Convert.ToInt32(ConfigurationManager.AppSettings["size"]), this.Height);
            width = int.Parse(ConfigurationManager.AppSettings["solPanelWidth"]);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            pcBoxMenu.ImageLocation = Path.Combine(ConfigurationManager.AppSettings["path"], "menu.png");
            ustPanel.Size = new Size(this.Width, height);
            solPanel.Size = new Size(width, this.Height);
            _ucsatis = new ucSatis();
            _ucsatis.Dock = DockStyle.Fill;
            
            anaPanel.Controls.Add(_ucsatis);
            AktifMenu(btnsatis);
            // guna2DragControl1.SetDrag(ustPanel);
        }

        private void anaPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        bool gizle = false;

        private void pcBoxMenu_Click(object sender, EventArgs e)
        {

            if (anaPanel.Controls.Count > 0 && anaPanel.Controls[0] is ucSatis satis)
            {
                if (gizle) // Eğer panel göürünür ise, size'ı ayarla
                    satis.flowsizechang(409, 544);
                else // Eğer panel gizli ise, size'ı ayarla
                    satis.flowsizechang(610, 544);
                satis.txtAra.Focus();
            }
            if (gizle)
            {
                solPanel.Width = width;
                gizle = false;
            }
            else
            {
                solPanel.Width = int.Parse(ConfigurationManager.AppSettings["solPanelWidthMini"]);
                gizle = true;
            }
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            Process.Start("osk.exe");
        }

        private Guna2Button ybuton = null;
        private bool AktifMenu(Guna2Button aktifButon)
        {
            if(ybuton == aktifButon)
                return false;
            
            foreach (Control c in solPanel.Controls)
            {
                if (c is Guna2Button btn)
                {
                    btn.FillColor = Color.Transparent;
                    btn.ForeColor = Color.White;
                }
            }

            aktifButon.FillColor = Color.FromArgb(0, 123, 255); // Aktif renk
            aktifButon.ForeColor = Color.White;
            ybuton = aktifButon;
            return true;
        }

        private void btnurunler_Click(object sender, EventArgs e)
        {
            if(!AktifMenu(btnurunler))
                return;
            ucUrunler ucUrunler = new ucUrunler();
            ucUrunler.Dock = DockStyle.Fill;
            anaPanel.Controls.Clear();
            anaPanel.Controls.Add(ucUrunler);

        }

        private void btnsatis_Click(object sender, EventArgs e)
        {
            
            if(!AktifMenu(btnsatis))
                return;
            ucSatis ucSatis = new ucSatis();
            ucSatis.Dock = DockStyle.Fill;
            anaPanel.Controls.Clear();
            anaPanel.Controls.Add(ucSatis);
            

        }

        private void btnkategoriler_Click(object sender, EventArgs e)
        {
            if (!AktifMenu(btnkategoriler))
                return;
            ucKategoriler ucKategoriler = new ucKategoriler();
            ucKategoriler.Dock = DockStyle.Fill;
            anaPanel.Controls.Clear();
            anaPanel.Controls.Add(ucKategoriler);

        }

        private void btnraporlar_Click(object sender, EventArgs e)
        {
            if (!AktifMenu(btnraporlar))
                return;
            ucRaporlar ucRaporlar = new ucRaporlar();
            ucRaporlar.Dock = DockStyle.Fill;
            anaPanel.Controls.Clear();
            anaPanel.Controls.Add(ucRaporlar);

        }

        private void btnSatisFis_Click(object sender, EventArgs e)
        {
            if (!AktifMenu(btnSatisFis))
                return;
            ucSatisFis ucSatisFis = new ucSatisFis();
            ucSatisFis.Dock = DockStyle.Fill;
            anaPanel.Controls.Clear();
            anaPanel.Controls.Add(ucSatisFis);
        }

        private void btnstok_Click(object sender, EventArgs e)
        {
            if (!AktifMenu(btnstok))
                return;
            ucStokHareketleri ucStokHareketleri = new ucStokHareketleri();
            ucStokHareketleri.Dock = DockStyle.Fill;
            anaPanel.Controls.Clear();
            anaPanel.Controls.Add(ucStokHareketleri);
        }

        private void zaman_Tick(object sender, EventArgs e)
        {
            lblTarihSaat.Text =$"Tarih : {DateTime.Now.ToString("dd.MM.yyyy")}             Saat : {DateTime.Now.ToString("HH:mm:ss")}";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmAnaSayfa_Shown(object sender, EventArgs e)
        {

        }
    }
}
