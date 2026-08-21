using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErgPosGuncelleyici
{
    
    public partial class Form1 : Form
    {
        private string downUrl;
        private string targetPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void ErgPOSBaslat()
        {
            string exe =
                Path.Combine(
                    targetPath + "\\ErgPOS",
                    "ErgPOS.exe"
                );

            if (File.Exists(exe))
            {
                Process.Start(exe);
            }
        }
        private void DosyalariKopyala(
     string kaynak,
     string hedef)
        {
            foreach (string file in
                     Directory.GetFiles(
                         kaynak,
                         "*",
                         SearchOption.AllDirectories))
            {
                string relativePath =
                    file.Substring(
                        kaynak.Length
                    ).TrimStart(
                        Path.DirectorySeparatorChar
                    );

                string destination =
                    Path.Combine(
                        hedef,
                        relativePath
                    );

                // Güncelleyici kendisini güncellemesin
                if (Path.GetFileName(destination)
                    .Equals(
                        "ErgPOSGuncelleyici.exe",
                        StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string directory =
                    Path.GetDirectoryName(
                        destination
                    );

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.Copy(
                    file,
                    destination,
                    true
                );
            }
        }

        private async Task ZipIndir(string zipPath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout =
                    TimeSpan.FromMinutes(10);

                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "ErgPOS-Updater"
                );

                using (
                    HttpResponseMessage response =
                    await client.GetAsync(
                        downUrl,
                        HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    long? total =
                        response.Content.Headers.ContentLength;

                    using (
                        Stream input =
                        await response.Content.ReadAsStreamAsync())

                    using (
                        FileStream output =
                        new FileStream(
                            zipPath,
                            FileMode.Create,
                            FileAccess.Write,
                            FileShare.None))
                    {
                        byte[] buffer =
                            new byte[81920];

                        long downloaded = 0;
                        int read;

                        while (
                            (read = await input.ReadAsync(
                                buffer,
                                0,
                                buffer.Length)) > 0)
                        {
                            await output.WriteAsync(
                                buffer,
                                0,
                                read);

                            downloaded += read;

                            if (total.HasValue)
                            {
                                int percent =
                                    (int)(
                                        downloaded * 100 /
                                        total.Value
                                    );

                                if (percent > 100)
                                    percent = 100;

                                progressBar1.Value =
                                    percent;

                                lblyuzde.Text =
                                    "%" + percent;
                            }
                        }
                    }
                }
            }
        }

        private void ErgPOSKapat()
        {
            Process[] processes =
                Process.GetProcessesByName("ErgPOS");

            foreach (Process process in processes)
            {
                try
                {
                    process.CloseMainWindow();

                    if (!process.WaitForExit(5000))
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                }
                catch
                {
                }
            }
        }
        private async Task Guncelle()
        {
            try
            {
                lblDurum.Text = "ErgPOS yazılımının kapanması bekleniyor...";

                await Task.Delay(1500);

                lblDurum.Text = "Güncelleme dosyaları indiriliyor...";

                string tempFolder = Path.Combine(
                    Path.GetTempPath(),
                    "ErgPOSUpdate"
                );

                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);

                Directory.CreateDirectory(tempFolder);

                string zipFilePath = Path.Combine(
                    tempFolder,
                    "ErgPOSRelease.zip"
                );

                await ZipIndir(zipFilePath);

                lblDurum.Text = "Güncelleme dosyaları açılıyor...";

                string extractPath = Path.Combine(
                    tempFolder,
                    "Extracted"
                );

                if (Directory.Exists(extractPath))
                    Directory.Delete(extractPath, true);

                Directory.CreateDirectory(extractPath);

                ZipFile.ExtractToDirectory(
                    zipFilePath,
                    extractPath
                );

                lblDurum.Text = "Güncelleme dosyaları kopyalanıyor...";

                DosyalariKopyala(
                    extractPath,
                    targetPath
                );

                lblDurum.Text =
                    "Güncelleme tamamlandı. ErgPOS başlatılıyor...";

                await Task.Delay(1000);

                ErgPOSBaslat();

                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Güncelleme sırasında bir hata oluştu:\n\n" +
                    ex.Message,
                    "ErgPOS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if(args.Length < 2)
                {
                    MessageBox.Show("Güncelleme adresi bulunamadı.", "ErgPOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                downUrl = args[1];
                targetPath = Path.Combine(Application.StartupPath, "..");
                targetPath = Path.GetFullPath(targetPath + "\\ErgPOS");
                await Guncelle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "ErgPOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
