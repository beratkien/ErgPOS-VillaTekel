namespace adisyon
{
    partial class ucSatis
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtAra = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.urunAlaniPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.sagPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblkalan = new System.Windows.Forms.Label();
            this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblToplamTutar = new System.Windows.Forms.Label();
            this.guna2TileButton1 = new Guna.UI2.WinForms.Guna2TileButton();
            this.btnYeniSepet = new Guna.UI2.WinForms.Guna2TileButton();
            this.btnSatissizIslem = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnTemizle = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnKart = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnNakit = new Guna.UI2.WinForms.Guna2GradientButton();
            this.label2 = new System.Windows.Forms.Label();
            this.sagPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAra
            // 
            this.txtAra.BorderRadius = 10;
            this.txtAra.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAra.DefaultText = "";
            this.txtAra.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAra.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAra.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAra.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAra.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAra.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAra.Location = new System.Drawing.Point(77, 50);
            this.txtAra.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtAra.Name = "txtAra";
            this.txtAra.PlaceholderText = "";
            this.txtAra.SelectedText = "";
            this.txtAra.Size = new System.Drawing.Size(500, 43);
            this.txtAra.TabIndex = 0;
            this.txtAra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAra_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(89, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Barkod / Ürün Ara";
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Blue;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(584, 50);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(101, 43);
            this.guna2Button1.TabIndex = 2;
            this.guna2Button1.Text = "Ara";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // urunAlaniPanel
            // 
            this.urunAlaniPanel.AutoScroll = true;
            this.urunAlaniPanel.BackColor = System.Drawing.SystemColors.Control;
            this.urunAlaniPanel.Location = new System.Drawing.Point(77, 178);
            this.urunAlaniPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.urunAlaniPanel.Name = "urunAlaniPanel";
            this.urunAlaniPanel.Size = new System.Drawing.Size(545, 670);
            this.urunAlaniPanel.TabIndex = 3;
            // 
            // sagPanel
            // 
            this.sagPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.sagPanel.BorderColor = System.Drawing.Color.White;
            this.sagPanel.Controls.Add(this.lblkalan);
            this.sagPanel.Controls.Add(this.guna2VScrollBar1);
            this.sagPanel.Controls.Add(this.lblToplamTutar);
            this.sagPanel.Controls.Add(this.guna2TileButton1);
            this.sagPanel.Controls.Add(this.btnYeniSepet);
            this.sagPanel.Controls.Add(this.btnSatissizIslem);
            this.sagPanel.Controls.Add(this.btnTemizle);
            this.sagPanel.Controls.Add(this.btnKart);
            this.sagPanel.Controls.Add(this.btnNakit);
            this.sagPanel.Controls.Add(this.flowLayoutPanel1);
            this.sagPanel.Controls.Add(this.label2);
            this.sagPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.sagPanel.Location = new System.Drawing.Point(1139, 0);
            this.sagPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sagPanel.Name = "sagPanel";
            this.sagPanel.Size = new System.Drawing.Size(541, 1070);
            this.sagPanel.TabIndex = 4;
            // 
            // lblkalan
            // 
            this.lblkalan.AutoSize = true;
            this.lblkalan.BackColor = System.Drawing.Color.Transparent;
            this.lblkalan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblkalan.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblkalan.Location = new System.Drawing.Point(5, 639);
            this.lblkalan.Name = "lblkalan";
            this.lblkalan.Size = new System.Drawing.Size(68, 17);
            this.lblkalan.TabIndex = 10;
            this.lblkalan.Text = "KALAN :";
            // 
            // guna2VScrollBar1
            // 
            this.guna2VScrollBar1.BindingContainer = this.flowLayoutPanel1;
            this.guna2VScrollBar1.InUpdate = false;
            this.guna2VScrollBar1.LargeChange = 10;
            this.guna2VScrollBar1.Location = new System.Drawing.Point(486, 220);
            this.guna2VScrollBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2VScrollBar1.Name = "guna2VScrollBar1";
            this.guna2VScrollBar1.ScrollbarSize = 21;
            this.guna2VScrollBar1.Size = new System.Drawing.Size(21, 482);
            this.guna2VScrollBar1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 220);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(507, 482);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // lblToplamTutar
            // 
            this.lblToplamTutar.AutoSize = true;
            this.lblToplamTutar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamTutar.ForeColor = System.Drawing.Color.Blue;
            this.lblToplamTutar.Location = new System.Drawing.Point(19, 609);
            this.lblToplamTutar.Name = "lblToplamTutar";
            this.lblToplamTutar.Size = new System.Drawing.Size(115, 25);
            this.lblToplamTutar.TabIndex = 3;
            this.lblToplamTutar.Text = "TOPLAM :";
            // 
            // guna2TileButton1
            // 
            this.guna2TileButton1.BorderRadius = 10;
            this.guna2TileButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2TileButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2TileButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TileButton1.ForeColor = System.Drawing.Color.White;
            this.guna2TileButton1.Location = new System.Drawing.Point(145, 50);
            this.guna2TileButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2TileButton1.Name = "guna2TileButton1";
            this.guna2TileButton1.Size = new System.Drawing.Size(128, 59);
            this.guna2TileButton1.TabIndex = 9;
            this.guna2TileButton1.Text = "Sepetleri Görüntüle";
            this.guna2TileButton1.Click += new System.EventHandler(this.guna2TileButton1_Click);
            // 
            // btnYeniSepet
            // 
            this.btnYeniSepet.BorderRadius = 10;
            this.btnYeniSepet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnYeniSepet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnYeniSepet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnYeniSepet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnYeniSepet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYeniSepet.ForeColor = System.Drawing.Color.White;
            this.btnYeniSepet.Location = new System.Drawing.Point(11, 50);
            this.btnYeniSepet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnYeniSepet.Name = "btnYeniSepet";
            this.btnYeniSepet.Size = new System.Drawing.Size(128, 59);
            this.btnYeniSepet.TabIndex = 8;
            this.btnYeniSepet.Text = "Yeni sepet";
            this.btnYeniSepet.Click += new System.EventHandler(this.btnYeniSepet_Click);
            // 
            // btnSatissizIslem
            // 
            this.btnSatissizIslem.BorderRadius = 10;
            this.btnSatissizIslem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSatissizIslem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSatissizIslem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSatissizIslem.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSatissizIslem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSatissizIslem.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.btnSatissizIslem.ForeColor = System.Drawing.Color.White;
            this.btnSatissizIslem.Location = new System.Drawing.Point(209, 670);
            this.btnSatissizIslem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSatissizIslem.Name = "btnSatissizIslem";
            this.btnSatissizIslem.Size = new System.Drawing.Size(204, 86);
            this.btnSatissizIslem.TabIndex = 7;
            this.btnSatissizIslem.Text = "SATIŞSIZ İŞLEM";
            this.btnSatissizIslem.Click += new System.EventHandler(this.guna2GradientButton1_Click);
            // 
            // btnTemizle
            // 
            this.btnTemizle.BorderRadius = 10;
            this.btnTemizle.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTemizle.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTemizle.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTemizle.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTemizle.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTemizle.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnTemizle.ForeColor = System.Drawing.Color.White;
            this.btnTemizle.Location = new System.Drawing.Point(3, 670);
            this.btnTemizle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(204, 86);
            this.btnTemizle.TabIndex = 6;
            this.btnTemizle.Text = "TEMİZLE";
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);
            // 
            // btnKart
            // 
            this.btnKart.BorderColor = System.Drawing.Color.DimGray;
            this.btnKart.BorderRadius = 10;
            this.btnKart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKart.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKart.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnKart.ForeColor = System.Drawing.Color.White;
            this.btnKart.Location = new System.Drawing.Point(209, 762);
            this.btnKart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKart.Name = "btnKart";
            this.btnKart.Size = new System.Drawing.Size(204, 86);
            this.btnKart.TabIndex = 5;
            this.btnKart.Text = "KART";
            this.btnKart.Click += new System.EventHandler(this.btnKart_Click);
            // 
            // btnNakit
            // 
            this.btnNakit.BorderRadius = 10;
            this.btnNakit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNakit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNakit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNakit.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNakit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNakit.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnNakit.ForeColor = System.Drawing.Color.White;
            this.btnNakit.Location = new System.Drawing.Point(3, 762);
            this.btnNakit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNakit.Name = "btnNakit";
            this.btnNakit.Size = new System.Drawing.Size(204, 86);
            this.btnNakit.TabIndex = 4;
            this.btnNakit.Text = "NAKİT";
            this.btnNakit.Click += new System.EventHandler(this.btnNakit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(5, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "SEPET (0)";
            // 
            // ucSatis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.sagPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.urunAlaniPanel);
            this.Controls.Add(this.txtAra);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucSatis";
            this.Size = new System.Drawing.Size(1680, 1070);
            this.Load += new System.EventHandler(this.ucSatis_Load);
            this.sagPanel.ResumeLayout(false);
            this.sagPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        public Guna.UI2.WinForms.Guna2Panel sagPanel;
        private Guna.UI2.WinForms.Guna2VScrollBar guna2VScrollBar1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblToplamTutar;
        private Guna.UI2.WinForms.Guna2GradientButton btnKart;
        private Guna.UI2.WinForms.Guna2GradientButton btnNakit;
        private Guna.UI2.WinForms.Guna2GradientButton btnTemizle;
        private Guna.UI2.WinForms.Guna2GradientButton btnSatissizIslem;
        private Guna.UI2.WinForms.Guna2TileButton btnYeniSepet;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2TileButton guna2TileButton1;
        public System.Windows.Forms.FlowLayoutPanel urunAlaniPanel;
        public Guna.UI2.WinForms.Guna2TextBox txtAra;
        public System.Windows.Forms.Label lblkalan;
    }
}
