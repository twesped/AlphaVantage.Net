// ReSharper disable InconsistentNaming
namespace Tw.AlphaVantage.Net.Stocks.Parsing.JsonTokens
{
    internal static class SymbolSearchJsonTokens
    {
        public const string SymbolSearchHeader = "bestMatches";

        public const string SymbolSearchSymbolToken = "1. symbol";
        public const string SymbolSearchNameToken = "2. name";
        public const string SymbolSearchTypeToken = "3. type";
        public const string SymbolSearchRegionToken = "4. region";
        public const string SymbolSearchMarketOpenToken = "5. marketOpen";
        public const string SymbolSearchMarketCloseToken = "6. marketClose";
        public const string SymbolSearchTimeZoneToken = "7. timezone";
        public const string SymbolSearchCurrencyToken = "8. currency";
        public const string SymbolSearchMatchScoreToken = "9. matchScore";
        
    }
}