using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using adisyon.Models;

namespace adisyon
{
    public partial class NakitSatis : Form
    {
        decimal tt_tutar;
        private ucSatis _ucsatis;
        public NakitSatis(decimal t_tutar, ucSatis ucsatis)
        {
           
            InitializeComponent();
            tt_tutar = t_tutar;
            _ucsatis = ucsatis;
        }
        public  bool _nakit;
        private void NakitSatis_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(_nakit.ToString());
            OdenenTutar = 0m;
            ParaUstu = 0m;
            btnSimdiKapat.Visible = false;
            lblSure.Text = "";
            guna2TextBox1.PlaceholderText = "0,00";
            lblIslemTutar.Text = $"İŞLEM TUTARI: {tt_tutar} TL";

        }
        int süre = int.Parse(ConfigurationManager.AppSettings["timer1"]);
        decimal p_ustu = 0;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text))
                return;
            if(!decimal.TryParse(guna2TextBox1.Text, out decimal girilenTutar))
            {
                template.ShowMessageInfo("Lütfen geçerli bir değer girin.", this);
                return;
            }
            
            if(_nakit)
            {
                if (Convert.ToDecimal(guna2TextBox1.Text) < tt_tutar)
                {
                    OdenenTutar = girilenTutar;
                    ParaUstu = 0m;
                    _ucsatis.kalanGoster(true, false, tt_tutar - girilenTutar, girilenTutar);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                OdenenTutar = girilenTutar;
                p_ustu = Convert.ToDecimal(guna2TextBox1.Text) - tt_tutar;
            }
            else
            {
                if(Convert.ToDecimal(guna2TextBox1.Text) > tt_tutar)
                {
                    
                    template.ShowMessageInfo("Girilen tutar işlem tutarından büyük olamaz.", this);
                    return;
                }
                if (Convert.ToDecimal(guna2TextBox1.Text) < tt_tutar)
                {
                    OdenenTutar = girilenTutar;
                    ParaUstu = 0m;
                    _ucsatis.kalanGoster(false, true, tt_tutar - girilenTutar, girilenTutar);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                OdenenTutar = girilenTutar;
                p_ustu = 0.00M;
            }

            ParaUstu = p_ustu;
            _ucsatis.kalanGoster(false, false, 0, 0);
            label1.Text = $"PARA ÜSTÜ: {p_ustu} TL";
            timer1.Start();
            btnSimdiKapat.Visible = true;
            btnTamamla.Enabled = false;
            guna2Button2.Enabled = false;
            guna2TextBox1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            süre--;
            lblSure.Text = $"Ekran {süre} saniye sonra kapanacaktır!";
            if(süre == 0)
            {
                timer1.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSimdiKapat_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "1";

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "2";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "3";
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "4";
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "5";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "6";
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "7";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "8";
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "9";
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += "0";
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += ",";
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text += ".";
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                guna2TextBox1.Text = guna2TextBox1.Text.Substring(0, guna2TextBox1.Text.Length - 1);
                guna2TextBox1.SelectionStart = guna2TextBox1.Text.Length;
            }
        }

        public decimal OdenenTutar { get; private set; }
        public decimal ParaUstu { get; private set; }
    }
}


