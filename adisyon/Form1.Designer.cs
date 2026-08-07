namespace adisyon
{
    partial class FrmAnaSayfa
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

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnaSayfa));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.solPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSatisFis = new Guna.UI2.WinForms.Guna2Button();
            this.logoPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnsatis = new Guna.UI2.WinForms.Guna2Button();
            this.btnkategoriler = new Guna.UI2.WinForms.Guna2Button();
            this.btnurunler = new Guna.UI2.WinForms.Guna2Button();
            this.btnstok = new Guna.UI2.WinForms.Guna2Button();
            this.btnraporlar = new Guna.UI2.WinForms.Guna2Button();
            this.ustPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTarihSaat = new System.Windows.Forms.Label();
            this.guna2TileButton1 = new Guna.UI2.WinForms.Guna2TileButton();
            this.pcBoxMenu = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.anaPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.zaman = new System.Windows.Forms.Timer(this.components);
            this.solPanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            this.ustPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBoxMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // solPanel
            // 
            this.solPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.solPanel.Controls.Add(this.label2);
            this.solPanel.Controls.Add(this.btnSatisFis);
            this.solPanel.Controls.Add(this.logoPanel);
            this.solPanel.Controls.Add(this.btnsatis);
            this.solPanel.Controls.Add(this.btnkategoriler);
            this.solPanel.Controls.Add(this.btnurunler);
            this.solPanel.Controls.Add(this.btnstok);
            this.solPanel.Controls.Add(this.btnraporlar);
            this.solPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.solPanel.Location = new System.Drawing.Point(0, 0);
            this.solPanel.Name = "solPanel";
            this.solPanel.Size = new System.Drawing.Size(226, 884);
            this.solPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Calligraphy", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 860);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 69);
            this.label2.TabIndex = 11;
            this.label2.Text = "İLETİŞİM\r\n\r\n05525857580";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnSatisFis
            // 
            this.btnSatisFis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSatisFis.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSatisFis.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSatisFis.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSatisFis.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSatisFis.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.btnSatisFis.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnSatisFis.ForeColor = System.Drawing.Color.White;
            this.btnSatisFis.Location = new System.Drawing.Point(-218, 732);
            this.btnSatisFis.Name = "btnSatisFis";
            this.btnSatisFis.Size = new System.Drawing.Size(658, 102);
            this.btnSatisFis.TabIndex = 10;
            this.btnSatisFis.Text = "Fiş Detayları";
            this.btnSatisFis.Click += new System.EventHandler(this.btnSatisFis_Click);
            // 
            // logoPanel
            // 
            this.logoPanel.Controls.Add(this.label1);
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(226, 141);
            this.logoPanel.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "ErgPOS";
            // 
            // btnsatis
            // 
            this.btnsatis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnsatis.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnsatis.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnsatis.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnsatis.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnsatis.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.btnsatis.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnsatis.ForeColor = System.Drawing.Color.White;
            this.btnsatis.Location = new System.Drawing.Point(-218, 166);
            this.btnsatis.Name = "btnsatis";
            this.btnsatis.Size = new System.Drawing.Size(658, 128);
            this.btnsatis.TabIndex = 0;
            this.btnsatis.Text = "Satış";
            this.btnsatis.Click += new System.EventHandler(this.btnsatis_Click);
            // 
            // btnkategoriler
            // 
            this.btnkategoriler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnkategoriler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnkategoriler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnkategoriler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnkategoriler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnkategoriler.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.btnkategoriler.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnkategoriler.ForeColor = System.Drawing.Color.White;
            this.btnkategoriler.Location = new System.Drawing.Point(-218, 408);
            this.btnkategoriler.Name = "btnkategoriler";
            this.btnkategoriler.Size = new System.Drawing.Size(658, 102);
            this.btnkategoriler.TabIndex = 2;
            this.btnkategoriler.Text = "Kategoriler";
            this.btnkategoriler.Click += new System.EventHandler(this.btnkategoriler_Click);
            // 
            // btnurunler
            // 
            this.btnurunler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnurunler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnurunler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnurunler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnurunler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnurunler.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.btnurunler.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnurunler.ForeColor = System.Drawing.Color.White;
            this.btnurunler.Location = new System.Drawing.Point(-218, 300);
            this.btnurunler.Name = "btnurunler";
            this.btnurunler.Size = new System.Drawing.Size(658, 102);
            this.btnurunler.TabIndex = 1;
            this.btnurunler.Text = "Ürünler";
            this.btnurunler.Click += new System.EventHandler(this.btnurunler_Click);
            // 
            // btnstok
            // 
            this.btnstok.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnstok.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnstok.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnstok.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnstok.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnstok.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.btnstok.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnstok.ForeColor = System.Drawing.Color.White;
            this.btnstok.Location = new System.Drawing.Point(-218, 624);
            this.btnstok.Name = "btnstok";
            this.btnstok.Size = new System.Drawing.Size(658, 102);
            this.btnstok.TabIndex = 4;
            this.btnstok.Text = "Stok Hareketleri";
            this.btnstok.Click += new System.EventHandler(this.btnstok_Click);
            // 
            // btnraporlar
            // 
            this.btnraporlar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnraporlar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnraporlar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnraporlar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnraporlar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnraporlar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.btnraporlar.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnraporlar.ForeColor = System.Drawing.Color.White;
            this.btnraporlar.Location = new System.Drawing.Point(-218, 516);
            this.btnraporlar.Name = "btnraporlar";
            this.btnraporlar.Size = new System.Drawing.Size(658, 102);
            this.btnraporlar.TabIndex = 3;
            this.btnraporlar.Text = "Raporlar";
            this.btnraporlar.Click += new System.EventHandler(this.btnraporlar_Click);
            // 
            // ustPanel
            // 
            this.ustPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.ustPanel.Controls.Add(this.lblTarihSaat);
            this.ustPanel.Controls.Add(this.guna2TileButton1);
            this.ustPanel.Controls.Add(this.pcBoxMenu);
            this.ustPanel.Controls.Add(this.guna2ControlBox3);
            this.ustPanel.Controls.Add(this.guna2ControlBox1);
            this.ustPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ustPanel.Location = new System.Drawing.Point(226, 0);
            this.ustPanel.Name = "ustPanel";
            this.ustPanel.Size = new System.Drawing.Size(1330, 138);
            this.ustPanel.TabIndex = 1;
            // 
            // lblTarihSaat
            // 
            this.lblTarihSaat.AutoSize = true;
            this.lblTarihSaat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTarihSaat.ForeColor = System.Drawing.Color.White;
            this.lblTarihSaat.Location = new System.Drawing.Point(109, 3);
            this.lblTarihSaat.Name = "lblTarihSaat";
            this.lblTarihSaat.Size = new System.Drawing.Size(62, 22);
            this.lblTarihSaat.TabIndex = 14;
            this.lblTarihSaat.Text = "zaman";
            // 
            // guna2TileButton1
            // 
            this.guna2TileButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2TileButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2TileButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2TileButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TileButton1.ForeColor = System.Drawing.Color.White;
            this.guna2TileButton1.Location = new System.Drawing.Point(792, 12);
            this.guna2TileButton1.Name = "guna2TileButton1";
            this.guna2TileButton1.Size = new System.Drawing.Size(177, 55);
            this.guna2TileButton1.TabIndex = 13;
            this.guna2TileButton1.Text = "Klavyeyi Aç";
            this.guna2TileButton1.Click += new System.EventHandler(this.guna2TileButton1_Click);
            // 
            // pcBoxMenu
            // 
            this.pcBoxMenu.BackColor = System.Drawing.Color.Transparent;
            this.pcBoxMenu.FillColor = System.Drawing.Color.Transparent;
            this.pcBoxMenu.ImageRotate = 0F;
            this.pcBoxMenu.Location = new System.Drawing.Point(6, 3);
            this.pcBoxMenu.Name = "pcBoxMenu";
            this.pcBoxMenu.Size = new System.Drawing.Size(71, 49);
            this.pcBoxMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBoxMenu.TabIndex = 12;
            this.pcBoxMenu.TabStop = false;
            this.pcBoxMenu.Click += new System.EventHandler(this.pcBoxMenu_Click);
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox3.Location = new System.Drawing.Point(1106, 12);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(103, 47);
            this.guna2ControlBox3.TabIndex = 10;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BorderColor = System.Drawing.Color.White;
            this.guna2ControlBox1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.guna2ControlBox1.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1215, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(103, 47);
            this.guna2ControlBox1.TabIndex = 10;
            // 
            // anaPanel
            // 
            this.anaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.anaPanel.Location = new System.Drawing.Point(226, 138);
            this.anaPanel.Name = "anaPanel";
            this.anaPanel.Size = new System.Drawing.Size(1330, 746);
            this.anaPanel.TabIndex = 3;
            this.anaPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.anaPanel_Paint);
            // 
            // zaman
            // 
            this.zaman.Enabled = true;
            this.zaman.Interval = 1000;
            this.zaman.Tick += new System.EventHandler(this.zaman_Tick);
            // 
            // FrmAnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 884);
            this.Controls.Add(this.anaPanel);
            this.Controls.Add(this.ustPanel);
            this.Controls.Add(this.solPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAnaSayfa";
            this.Text = "Anasayfa";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.FrmAnaSayfa_Shown);
            this.solPanel.ResumeLayout(false);
            this.solPanel.PerformLayout();
            this.logoPanel.ResumeLayout(false);
            this.logoPanel.PerformLayout();
            this.ustPanel.ResumeLayout(false);
            this.ustPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBoxMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Panel solPanel;
        private Guna.UI2.WinForms.Guna2Button btnsatis;
        private Guna.UI2.WinForms.Guna2Button btnurunler;
        private Guna.UI2.WinForms.Guna2Button btnstok;
        private Guna.UI2.WinForms.Guna2Button btnraporlar;
        private Guna.UI2.WinForms.Guna2Button btnkategoriler;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel ustPanel;
        private Guna.UI2.WinForms.Guna2Panel anaPanel;
        private Guna.UI2.WinForms.Guna2Panel logoPanel;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2PictureBox pcBoxMenu;
        private Guna.UI2.WinForms.Guna2TileButton guna2TileButton1;
        private Guna.UI2.WinForms.Guna2Button btnSatisFis;
        private System.Windows.Forms.Timer zaman;
        private System.Windows.Forms.Label lblTarihSaat;
        private System.Windows.Forms.Label label2;
    }
}

