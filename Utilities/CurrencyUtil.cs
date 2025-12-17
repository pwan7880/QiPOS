using System; 
using System.Globalization;
using System.Windows.Forms;

namespace QiPOS
{
    public static class CurrencyUtil
    {
        /// <summary>
        /// Converts a decimal value to a currency-formatted string. Example: 12.5 → "$12.50"
        /// </summary>
        public static string ToCurrencyString(decimal value)
        {
            return string.Format(CultureInfo.CurrentCulture, UIStyles.CurrencyFormat, value);
        }

        /// <summary>
        /// Safely parses a currency-formatted string into a decimal.
        /// Handles: "$12.50", "(15.25)", "1,234.56", etc.
        /// Returns 0 if invalid.
        /// </summary>
        public static decimal ParseCurrency(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0m;

            input = input.Trim();

            // Handle (x) for negative amounts
            if (input.StartsWith("(") && input.EndsWith(")"))
                input = "-" + input.Substring(1, input.Length - 2);

            var cleaned = input
                .Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol, "")
                .Replace(",", "")
                .Replace("$", "")
                .Replace(" ", "");

            return decimal.TryParse(cleaned, NumberStyles.Number | NumberStyles.AllowLeadingSign, CultureInfo.CurrentCulture, out var result)
                ? result
                : 0m;
        }

        /// <summary>
        /// Converts any object (e.g., from grid cell) to a decimal if possible.
        /// </summary>
        public static decimal SafeToDecimal(object value)
        {
            return value == null ? 0m : ParseCurrency(value.ToString());
        }

        /// <summary>
        /// Try-parse version for safer validation
        /// </summary>
        public static bool TryParseCurrency(string input, out decimal value)
        {
            value = ParseCurrency(input);
            return value != 0m;
        }

        /// <summary>
        /// Handles KeyPress input for live decimal entry (e.g., txtAmount), simulating calculator input.
        /// Applies optional rate modifier for refunds/discounts.
        /// </summary>
        public static decimal ValidateCurrencyInput(string currentText, KeyPressEventArgs e, decimal rateModifier = 1m)
        {
            bool isNegative = false;
            string stripped = currentText.Trim()
                                         .Replace(".", "")
                                         .Replace("$", "")
                                         .Replace(",", "");

            if (stripped.Contains("("))
            {
                isNegative = true;
                stripped = stripped.Replace("(", "").Replace(")", "");
            }

            decimal value = 0m;
            if (decimal.TryParse(stripped, out decimal num))
                value = num / 100;

            int key = e.KeyChar;

            switch (key)
            {
                case 46: // dot
                    e.Handled = true;
                    value *= 100;
                    break;

                case 45: // minus
                    if (value == 0)
                    {
                        e.Handled = true;
                        value = -value;
                    }
                    break;

                default:
                    if (key == 8 || key == 27 || key == 32 || stripped.Length > 8)
                    {
                        value = 0;
                        e.Handled = true;
                    }
                    else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        int numKey = key - 48;
                        value = value >= 0
                            ? (value * 1000 + numKey) / 100
                            : (value * 1000 - numKey) / 100;
                        e.Handled = true;
                    }
                    break;
            }

            if (isNegative)
                value = -value;

            return value * rateModifier;
        }

        
        public static Decimal StringToDecimalValidation(string strAmount, KeyPressEventArgs e)
        {
            bool flag = false;
            strAmount = strAmount.Trim();
            if (strAmount.IndexOf("(") >= 0)
            {
                flag = true;
                strAmount = strAmount.Replace("(", UIStyles.Empty);
                strAmount = strAmount.Replace(")", UIStyles.Empty);
            }
            strAmount = strAmount.Replace(".", UIStyles.Empty);
            strAmount = strAmount.Replace("$", UIStyles.Empty);
            strAmount = strAmount.Replace(",", UIStyles.Empty);
            int length = strAmount.Length;
            Decimal num1 = new Decimal(0);
            if (strAmount != UIStyles.Empty)
                num1 = Convert.ToDecimal(strAmount);
            Decimal num2 = num1 / new Decimal(100);
            int num3 = (int)e.KeyChar;
            int num4;
            switch (num3)
            {
                case 46:
                    e.Handled = true;
                    num2 *= new Decimal(100);
                    goto label_15;
                case 45:
                    num4 = !(num2 != new Decimal(0)) ? 1 : 0;
                    break;
                default:
                    num4 = 1;
                    break;
            }
            if (num4 == 0)
            {
                e.Handled = true;
                num2 = new Decimal(0) - num2;
            }
            else if (num3 == 8 || num3 == 27 || num3 == 32 || length > 8)
            {
                num2 = new Decimal(0);
                e.Handled = true;
            }
            else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || (num3 == 47 || num3 < 45) || num3 > 58)
            {
                e.Handled = true;
            }
            else
            {
                int num5 = num3 - 48;
                num2 = !(num2 >= new Decimal(0)) ? (num2 * new Decimal(1000) - (Decimal)num5) / new Decimal(100) : (num2 * new Decimal(1000) + (Decimal)num5) / new Decimal(100);
                e.Handled = true;
            }
        label_15:
            if (flag)
                num2 = new Decimal(0) - num2;
            return num2;
        }
    }
}
