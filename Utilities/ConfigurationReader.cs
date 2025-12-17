using System;
using System.IO;
using System.Linq;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Configuration;

namespace QiPOS
{
    /// <summary>
    /// reads the configuration data, using a C# struct
    /// </summary>
    public class CompanyData
    {
        public string PosPrinter;
        public string LineDisplayPort;
        public string CompanyName;
        public string CompanyABN;
        public string AddressLine1;
        public string AddressCity;
        public string AddressCity2;
        public string Telephone;
        public string Fax;
        public string Email;
    }

    /// <summary>
    /// The reader now uses app.config instead of ini
    /// </summary>
    public class ConfigurationReader
    {
        /// <summary>
        /// constructor checks if the configuration is valid
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ConfigIOException"></exception>
        public CompanyData CompanyInfo()
        {
            try
            {
                return GetConfig();
            }
            catch (Exception ex)
            {
                throw new ConfigIOException("Unexpected error while parsing config.", ex);
            }
        }

        /// <summary>
        /// Saves to app.config with validation
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ConfigIOException"></exception>
        public void SaveConfig(CompanyData data)
        {
            try
            {
                ValidateData(data);
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                UpdateAppSetting(config, "PosPrinter", data.PosPrinter);
                UpdateAppSetting(config, "LineDisplayPort", data.LineDisplayPort);
                UpdateAppSetting(config, "CompanyName", data.CompanyName);
                UpdateAppSetting(config, "CompanyABN", data.CompanyABN);
                UpdateAppSetting(config, "AddressLine1", data.AddressLine1);
                UpdateAppSetting(config, "AddressCity", data.AddressCity);
                UpdateAppSetting(config, "AddressCity2", data.AddressCity2);
                UpdateAppSetting(config, "Telephone", data.Telephone);
                UpdateAppSetting(config, "Fax", data.Fax);
                UpdateAppSetting(config, "Email", data.Email);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                throw new ConfigIOException("Failed to save config.", ex);
            }
        }

        private void UpdateAppSetting(System.Configuration.Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value ?? "");
            }
            else
            {
                config.AppSettings.Settings[key].Value = value ?? "";
            }
        }

        /// <summary>
        /// Get configuration data from app.config with validation.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ConfigIOException"></exception>
        private CompanyData GetConfig()
        {
            try
            {
                var data = new CompanyData
                {
                    PosPrinter = ConfigurationManager.AppSettings["PosPrinter"] ?? "",
                    LineDisplayPort = ConfigurationManager.AppSettings["LineDisplayPort"] ?? "",
                    CompanyName = ConfigurationManager.AppSettings["CompanyName"] ?? "",
                    CompanyABN = ConfigurationManager.AppSettings["CompanyABN"] ?? "",
                    AddressLine1 = ConfigurationManager.AppSettings["AddressLine1"] ?? "",
                    AddressCity = ConfigurationManager.AppSettings["AddressCity"] ?? "",
                    AddressCity2 = ConfigurationManager.AppSettings["AddressCity2"] ?? "",
                    Telephone = ConfigurationManager.AppSettings["Telephone"] ?? "",
                    Fax = ConfigurationManager.AppSettings["Fax"] ?? "",
                    Email = ConfigurationManager.AppSettings["Email"] ?? ""
                };

                ValidateData(data);

                if (string.IsNullOrWhiteSpace(data.PosPrinter))
                    throw new ConfigIOException("POS printer name is missing from config.");

                // Hack: if a file called 'devmode.flag' exists, skip printer check
                bool devMode = File.Exists("devmode.flag");

                if (!devMode)
                {
                    bool printerExists = PrinterSettings.InstalledPrinters
                        .Cast<string>()
                        .Any(name => name.Equals(data.PosPrinter, StringComparison.OrdinalIgnoreCase));

                    if (!printerExists)
                        throw new ConfigIOException($"Configured POS printer '{data.PosPrinter}' is not installed on this machine.");
                }
                else
                {
                    ErrorLogWriter.Instance.Log("⚠ Dev mode active: skipping PosPrinter check");
                }

                // 💡 Check LineDisplayPort format: must be COM followed by digits
                if (!string.IsNullOrWhiteSpace(data.LineDisplayPort))
                {
                    if (!Regex.IsMatch(data.LineDisplayPort, @"^COM\d+$", RegexOptions.IgnoreCase))
                    {
                        Console.WriteLine("Warning: LineDisplayPort value appears malformed: " + data.LineDisplayPort);
                        data.LineDisplayPort = ""; // blank it to avoid accidents
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new ConfigIOException("Unexpected error while parsing config.", ex);
            }
        }

        private void ValidateData(CompanyData data)
        {
            // Email validation
            if (!string.IsNullOrWhiteSpace(data.Email) && !Regex.IsMatch(data.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ConfigIOException("Invalid email address format.");
            }

            // ABN validation (11 digits)
            if (!string.IsNullOrWhiteSpace(data.CompanyABN) && !Regex.IsMatch(data.CompanyABN, @"^\d{11}$"))
            {
                throw new ConfigIOException("ABN must be exactly 11 digits.");
            }

            // Telephone/Fax validation (10-digit Australian format, e.g., 02 9878 1666)
            string phonePattern = @"^\d{2}\s\d{4}\s\d{4}$";
            if (!string.IsNullOrWhiteSpace(data.Telephone) && !Regex.IsMatch(data.Telephone, phonePattern))
            {
                throw new ConfigIOException("Telephone must be in format XX XXXX XXXX (e.g., 02 9878 1666).");
            }
            if (!string.IsNullOrWhiteSpace(data.Fax) && !Regex.IsMatch(data.Fax, phonePattern))
            {
                throw new ConfigIOException("Fax must be in format XX XXXX XXXX (e.g., 02 9878 1666).");
            }

            // Company Name validation
            if (string.IsNullOrWhiteSpace(data.CompanyName) || data.CompanyName.Length > 100)
            {
                throw new ConfigIOException("Company Name is required and must not exceed 100 characters.");
            }

            // Address validation
            if (string.IsNullOrWhiteSpace(data.AddressLine1) || data.AddressLine1.Length > 100)
            {
                throw new ConfigIOException("Address Line 1 is required and must not exceed 100 characters.");
            }
            if (!string.IsNullOrWhiteSpace(data.AddressCity) && data.AddressCity.Length > 100)
            {
                throw new ConfigIOException("Address City must not exceed 100 characters.");
            }
            if (!string.IsNullOrWhiteSpace(data.AddressCity2) && data.AddressCity2.Length > 100)
            {
                throw new ConfigIOException("Address City 2 must not exceed 100 characters.");
            }

            // Printer/Display validation (non-empty already checked elsewhere)
        }
    }

    /// <summary>
    /// special exception for configuration I/O errors
    /// </summary>
    public class ConfigIOException : Exception
    {
        public ConfigIOException(string message, Exception inner = null)
            : base(message, inner)
        {
        }
    }
}