
namespace QiPOS
{
    public static class DisplayPoleSymbols
    {
        public const string Escape = "\x1B";
        public const string CarriageReturn = "\r";
        public const string VendorCommandQA = "QA";
        public const string VendorCommandQB = "QB";
        public const string DisplayTotalPrefix = Escape + "QA";
        public const string DisplayChangePrefix = Escape + "QB";
        public const string Clear =  "\f";
    }
}