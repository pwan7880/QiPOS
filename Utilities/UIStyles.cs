using System.Drawing;

namespace QiPOS
{
    public static class UIStyles
    {
        #region Constants
        public const string ReadyForNewSale = "Ready for a New Sale";
        public const string ChangeDefault = "Change: $0.00";
        public const string ZeroDollarString = "$0.00";
        public const string CashReceived = "Cash Received";
        public const string ItemValueGreaterThan500 = "Item Value Great Than $500   ";
        public const string ItemValue = " Item Value ";
        public const string TotalPrefix = "TOTAL";
        public const string ChangePrefix = "CHANGE";
        public const string ChangeColumn = "Change:";
        public const string SubTotalPrefix = "SubTotal";
        public const string LottoDescription = "Lotto";
        public const string AddNewItemNow = "Add New Item Now?";
        public const string SevenZero = "0000000";
        public const string CurrencyFormat = "{0:C}";
        public const string PointOfSale = "Point of Sale";
        public const string NotStockedItem = "Not Stocked Item";
        public const string ZineReturnWeek = "ARE Return Week: ";
        public const string DrawerOpenCommand = "\x1Bx70\x00\x80";
        public const string DefaultPrinterName = "CITIZEN CT-S310";
        public const string PoleDisplayClearCommand = "\f";
        public const string EscPosCenterAlign = "\x001Ba\x01";
        public const string EscPosLeftAlign = "\x001Ba\x00";
        public const string CarriageReturnLineFeed = "\r\n";
        public const string PaymentEFTPOS = "EFTPOS";
        public const string ShortDateFormat = "dd/MM/yy";
        public const string WeekString = "Week ";
        public const string NetString = " Net: ";
        public const string PaymentCash = "Cash";
        public const string ErrorInsufficientSale = "Insufficient sale detail rows.";
        public const string Empty = "";
        public const string TwoNewlines = "\n\n";
        public const int DefaultRowCount = 14;
        public const string LottoPrefix = "2874";

        #endregion

        #region Keys
        public static class Key
        {
            public const int Enter = 13;
            public const int Escape = 27;
            public const int Delete = 46;
            public const int Save = 83;
            public const int Refund = 82;
            public const int ArrowUp = 38;
            public const int ArrowDown = 40;
            public const int ArrowLeft = 37;
            public const int ArrowRight = 39;
            public const int F1 = 112;
            public const int F2 = 113;
            public const int F3 = 114;
            public const int F4 = 115;
            public const int F5 = 116;
            public const int F6 = 117;
            public const int F7 = 118;
            public const int F8 = 119;
            public const int F9 = 120;
            public const int F10 = 121;
            public const int F11 = 122;
            public const int F12 = 123;
        }
        #endregion

        #region fonts
        public static readonly Font FontHeader18 = new Font("Segoe UI", 18f, FontStyle.Regular);
        public static readonly Font FontCell16 = new Font("Segoe UI", 16f, FontStyle.Regular);
        #endregion
    }

}