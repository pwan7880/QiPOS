using System; 
using System.IO.Ports;
using System.Threading.Tasks; 

namespace QiPOS
{
    public static class DisplayPoleUtil
    {

        private static DateTime _lastDisplayErrorTime = DateTime.MinValue;
        private static bool _displayErrorLogged = false;
        public static void ShowTotalAndChange(string totalText, string changeText, string portName)
        {
            if (string.IsNullOrWhiteSpace(portName)) return;

            string totalLine = FormatLabelValue(UIStyles.TotalPrefix, totalText);
            string changeLine = FormatLabelValue(UIStyles.ChangePrefix, changeText);
            SendToDisplay(portName, DisplayPoleSymbols.Escape +  DisplayPoleSymbols.VendorCommandQA + totalLine + DisplayPoleSymbols.CarriageReturn + DisplayPoleSymbols.Escape + DisplayPoleSymbols.VendorCommandQB  + changeLine + DisplayPoleSymbols.CarriageReturn);
        }

        public static void ShowItemAndSubtotal(string description, decimal itemPrice, decimal subTotal, string portName)
        {
            if (string.IsNullOrWhiteSpace(portName)) return;

            string line1 = FormatItemLine(description, itemPrice);
            string line2 = FormatLabelValue(UIStyles.SubTotalPrefix, subTotal.ToString("C"));
            SendToDisplay(portName, DisplayPoleSymbols.Escape+ DisplayPoleSymbols.VendorCommandQA + line1 + DisplayPoleSymbols.CarriageReturn + DisplayPoleSymbols.Escape + DisplayPoleSymbols.VendorCommandQB + line2 + DisplayPoleSymbols.CarriageReturn);
        }

        public static void ClearDisplay(string portName)
        {
            if (string.IsNullOrWhiteSpace(portName)) return;
            SendToDisplay(portName, DisplayPoleSymbols.Clear);
        }

        private static string FormatItemLine(string desc, decimal price)
        {
            string priceStr = price.ToString("C").Trim();
            int space = 20 - priceStr.Length - 1;

            if (desc.Length > space)
                desc = desc.Substring(0, space);

            return desc.PadRight(space) + " " + priceStr;
        }

        private static string FormatLabelValue(string label, string value)
        {
            value = value.Trim();
            if (value.StartsWith(UIStyles.ChangeColumn))
                value = value.Substring(value.IndexOf('$'));

            string paddedValue = value.PadLeft(14);
            return label + paddedValue;
        }

        public static void SendToDisplay(string portName, string message)
        {
            Task.Run(() =>
            {
                try
                {
                    using (SerialPort port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One))
                    {
                        if (!port.IsOpen)
                            port.Open();
                        port.Write(message);
                        _displayErrorLogged = false; // reset error flag on success
                    }
                }
                catch (Exception ex)
                {
                    if (!_displayErrorLogged || (DateTime.Now - _lastDisplayErrorTime).TotalMinutes > 5)
                    {
                        ErrorLogWriter.Instance.Log(ex, $"Pole display error: {ex.Message}");
                        _lastDisplayErrorTime = DateTime.Now;
                        _displayErrorLogged = true;
                    }
                }
            });
        }

    }
}
