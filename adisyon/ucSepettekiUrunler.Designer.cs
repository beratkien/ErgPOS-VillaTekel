namespace adisyon
{
    partial class ucSepettekiUrunler
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
            this.lblS_UrunFiyat = new System.Windows.Forms.Label();
            this.lblS_UrunName = new System.Windows.Forms.Label();
            this.lblS_UrunTutar = new System.Windows.Forms.Label();
            this.lblS_UrunAdet = new System.Windows.Forms.Label();
            this.btnAzalt = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnArttir = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnSil = new Guna.UI2.WinForms.Guna2CircleButton();
            this.SuspendLayout();
            // 
            // lblS_UrunFiyat
            // 
            this.lblS_UrunFiyat.AutoSize = true;
            this.lblS_UrunFiyat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblS_UrunFiyat.Location = new System.Drawing.Point(4, 64);
            this.lblS_UrunFiyat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS_UrunFiyat.Name = "lblS_UrunFiyat";
            this.lblS_UrunFiyat.Size = new System.Drawing.Size(38, 17);
            this.lblS_UrunFiyat.TabIndex = 0;
            this.lblS_UrunFiyat.Text = "Fiyat";
            this.lblS_UrunFiyat.Click += new System.EventHandler(this.lblS_UrunFiyat_Click);
            // 
            // lblS_UrunName
            // 
            this.lblS_UrunName.AutoEllipsis = true;
            this.lblS_UrunName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblS_UrunName.Location = new System.Drawing.Point(4, 15);
            this.lblS_UrunName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS_UrunName.Name = "lblS_UrunName";
            this.lblS_UrunName.Size = new System.Drawing.Size(231, 14);
            this.lblS_UrunName.TabIndex = 1;
            this.lblS_UrunName.Text = "İsim";
            // 
            // lblS_UrunTutar
            // 
            this.lblS_UrunTutar.AutoSize = true;
            this.lblS_UrunTutar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblS_UrunTutar.Location = new System.Drawing.Point(4, 89);
            this.lblS_UrunTutar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS_UrunTutar.Name = "lblS_UrunTutar";
            this.lblS_UrunTutar.Size = new System.Drawing.Size(42, 17);
            this.lblS_UrunTutar.TabIndex = 3;
            this.lblS_UrunTutar.Text = "Tutar";
            // 
            // lblS_UrunAdet
            // 
            this.lblS_UrunAdet.AutoSize = true;
            this.lblS_UrunAdet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblS_UrunAdet.Location = new System.Drawing.Point(4, 39);
            this.lblS_UrunAdet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS_UrunAdet.Name = "lblS_UrunAdet";
            this.lblS_UrunAdet.Size = new System.Drawing.Size(37, 17);
            this.lblS_UrunAdet.TabIndex = 4;
            this.lblS_UrunAdet.Text = "Adet";
            // 
            // btnAzalt
            // 
            this.btnAzalt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAzalt.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAzalt.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAzalt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAzalt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAzalt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAzalt.ForeColor = System.Drawing.Color.White;
            this.btnAzalt.Location = new System.Drawing.Point(357, 43);
            this.btnAzalt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAzalt.Name = "btnAzalt";
            this.btnAzalt.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnAzalt.Size = new System.Drawing.Size(37, 37);
            this.btnAzalt.TabIndex = 5;
            this.btnAzalt.Text = "-";
            this.btnAzalt.Click += new System.EventHandler(this.btnAzalt_Click);
            // 
            // btnArttir
            // 
            this.btnArttir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArttir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnArttir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnArttir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnArttir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnArttir.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnArttir.ForeColor = System.Drawing.Color.White;
            this.btnArttir.Location = new System.Drawing.Point(357, 2);
            this.btnArttir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnArttir.Name = "btnArttir";
            this.btnArttir.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnArttir.Size = new System.Drawing.Size(37, 37);
            this.btnArttir.TabIndex = 6;
            this.btnArttir.Text = "+";
            this.btnArttir.Click += new System.EventHandler(this.btnArttir_Click);
            // 
            // btnSil
            // 
            this.btnSil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSil.FillColor = System.Drawing.Color.Red;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(357, 85);
            this.btnSil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSil.Name = "btnSil";
            this.btnSil.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnSil.Size = new System.Drawing.Size(37, 37);
            this.btnSil.TabIndex = 7;
            this.btnSil.Text = "X";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // ucSepettekiUrunler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnArttir);
            this.Controls.Add(this.btnAzalt);
            this.Controls.Add(this.lblS_UrunAdet);
            this.Controls.Add(this.lblS_UrunTutar);
            this.Controls.Add(this.lblS_UrunName);
            this.Controls.Add(this.lblS_UrunFiyat);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucSepettekiUrunler";
            this.Size = new System.Drawing.Size(404, 123);
            this.Load += new System.EventHandler(this.ucSepettekiUrunler_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblS_UrunFiyat;
        public System.Windows.Forms.Label lblS_UrunName;
        public System.Windows.Forms.Label lblS_UrunTutar;
        public System.Windows.Forms.Label lblS_UrunAdet;
        private Guna.UI2.WinForms.Guna2CircleButton btnAzalt;
        private Guna.UI2.WinForms.Guna2CircleButton btnArttir;
        private Guna.UI2.WinForms.Guna2CircleButton btnSil;
    }
}
