using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace adisyon.Models
{
    public class UpdateInfo
    {
        public string Version { get; set; }

        public string DownloadUrl { get; set; }

        public string Notes { get; set; }
    }

    public static class UpdateManager
    {
        private const string GitHubApi =
            "https://api.github.com/repos/bekayazilimofficial/ergpos/releases/latest";

        public static async Task<UpdateInfo> CheckForUpdate()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout =
                        TimeSpan.FromSeconds(3);

                    client.DefaultRequestHeaders.Add(
                        "User-Agent",
                        "ErgPOS"
                    );

                    string json =
                        await client.GetStringAsync(
                            GitHubApi
                        );

                    JObject release =
                        JObject.Parse(json);

                    string version =
                        release["tag_name"]
                        ?.ToString()
                        ?.TrimStart('v');
                    //MessageBox.Show("Yeni sürüm bulundu: " + version);
                    string notes =
                        release["body"]
                        ?.ToString();

                    JArray assets =
                        release["assets"] as JArray;

                    string downloadUrl = null;

                    if (assets != null)
                    {
                        foreach (JObject asset in assets)
                        {
                            string name =
                                asset["name"]?.ToString();

                            if (name ==
                                "ErgPOSRelease.zip")
                            {
                                downloadUrl =
                                    asset[
                                        "browser_download_url"
                                    ]?.ToString();

                                break;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(version) ||
                        string.IsNullOrEmpty(downloadUrl))
                    {
                        return null;
                    }

                    return new UpdateInfo
                    {
                        Version = version,
                        DownloadUrl = downloadUrl,
                        Notes = notes
                    };
                }
            }
            catch
            {
                // İnternet yoksa veya GitHub'a
                // ulaşılamıyorsa POS çalışmaya devam eder.
                return null;
            }
        }
    }
}
