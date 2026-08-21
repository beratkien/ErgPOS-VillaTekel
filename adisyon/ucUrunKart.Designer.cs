namespace adisyon
{
    partial class ucUrunKart
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
            this.urunResim = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblUrunIsim = new System.Windows.Forms.Label();
            this.lblFiyat = new System.Windows.Forms.Label();
            this.lblStok = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)(this.urunResim)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // urunResim
            // 
            this.urunResim.Dock = System.Windows.Forms.DockStyle.Top;
            this.urunResim.ImageRotate = 0F;
            this.urunResim.Location = new System.Drawing.Point(0, 0);
            this.urunResim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.urunResim.Name = "urunResim";
            this.urunResim.Size = new System.Drawing.Size(253, 169);
            this.urunResim.TabIndex = 0;
            this.urunResim.TabStop = false;
            this.urunResim.Click += new System.EventHandler(this.urunResim_Click);
            // 
            // lblUrunIsim
            // 
            this.lblUrunIsim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUrunIsim.AutoEllipsis = true;
            this.lblUrunIsim.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUrunIsim.Location = new System.Drawing.Point(3, 187);
            this.lblUrunIsim.Name = "lblUrunIsim";
            this.lblUrunIsim.Size = new System.Drawing.Size(212, 30);
            this.lblUrunIsim.TabIndex = 1;
            this.lblUrunIsim.Text = "İsim Olcak";
            this.lblUrunIsim.Click += new System.EventHandler(this.lblUrunIsim_Click);
            // 
            // lblFiyat
            // 
            this.lblFiyat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFiyat.AutoSize = true;
            this.lblFiyat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFiyat.ForeColor = System.Drawing.Color.Blue;
            this.lblFiyat.Location = new System.Drawing.Point(3, 228);
            this.lblFiyat.Name = "lblFiyat";
            this.lblFiyat.Size = new System.Drawing.Size(121, 26);
            this.lblFiyat.TabIndex = 2;
            this.lblFiyat.Text = "Fiyat Olcak";
            this.lblFiyat.Click += new System.EventHandler(this.lblFiyat_Click);
            // 
            // lblStok
            // 
            this.lblStok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStok.AutoSize = true;
            this.lblStok.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStok.Location = new System.Drawing.Point(3, 272);
            this.lblStok.Name = "lblStok";
            this.lblStok.Size = new System.Drawing.Size(118, 26);
            this.lblStok.TabIndex = 3;
            this.lblStok.Text = "Stok Olcak";
            this.lblStok.Click += new System.EventHandler(this.lblStok_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.urunResim);
            this.guna2Panel1.Controls.Add(this.lblStok);
            this.guna2Panel1.Controls.Add(this.lblUrunIsim);
            this.guna2Panel1.Controls.Add(this.lblFiyat);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(253, 345);
            this.guna2Panel1.TabIndex = 4;
            this.guna2Panel1.Click += new System.EventHandler(this.guna2Panel1_Click);
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // ucUrunKart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucUrunKart";
            this.Size = new System.Drawing.Size(253, 345);
            this.Load += new System.EventHandler(this.ucUrunKart_Load);
            this.Click += new System.EventHandler(this.ucUrunKart_Click);
            ((System.ComponentModel.ISupportInitialize)(this.urunResim)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Guna.UI2.WinForms.Guna2PictureBox urunResim;
        public System.Windows.Forms.Label lblUrunIsim;
        public System.Windows.Forms.Label lblFiyat;
        public System.Windows.Forms.Label lblStok;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
