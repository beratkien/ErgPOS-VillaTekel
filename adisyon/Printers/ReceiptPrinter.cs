using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Linq;

namespace adisyon.Printers
{
    public static class ReceiptPrinter
    {
        private const string ConfigKey = "ReceiptPrinterName";

        public static IEnumerable<string> GetInstalledPrinters()
        {
            return PrinterSettings.InstalledPrinters
                .Cast<string>()
                .OrderBy(x => x);
        }

        public static bool PrinterExists(string printerName)
        {
            if (string.IsNullOrWhiteSpace(printerName))
                return false;

            return PrinterSettings.InstalledPrinters
                .Cast<string>()
                .Any(x => string.Equals(x, printerName, StringComparison.OrdinalIgnoreCase));
        }

        public static string GetSelectedPrinterName()
        {
            string printerName = ConfigurationManager.AppSettings[ConfigKey];
            return string.IsNullOrWhiteSpace(printerName) ? null : printerName;
        }

        public static bool HasSelectedPrinter()
        {
            return !string.IsNullOrWhiteSpace(GetSelectedPrinterName());
        }

        public static void SetSelectedPrinterName(string printerName)
        {
            if (string.IsNullOrWhiteSpace(printerName))
                return;
            //throw new ArgumentException("printerName boş olamaz.", "printerName");

            if (!PrinterExists(printerName))
                return;
                //throw new InvalidOperationException("Seçilen yazıcı sistemde bulunamadı: " + printerName);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[ConfigKey] == null)
                config.AppSettings.Settings.Add(ConfigKey, printerName);
            else
                config.AppSettings.Settings[ConfigKey].Value = printerName;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ClearSelectedPrinterName()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[ConfigKey] != null)
                config.AppSettings.Settings.Remove(ConfigKey);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void EnsureSelectedPrinter()
        {
            if (!HasSelectedPrinter())
                return;
               // throw new InvalidOperationException("Önce fiş yazıcısı seçilmelidir.");

            string printerName = GetSelectedPrinterName();

            if (!PrinterExists(printerName))
                return;
                //throw new InvalidOperationException("Seçili fiş yazıcısı sistemde bulunamadı: " + printerName);
        }
    }
}
    