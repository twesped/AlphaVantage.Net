// ReSharper disable InconsistentNaming
namespace Tw.AlphaVantage.Net.Stocks.Parsing.JsonTokens
{
    internal static class QuoteEndpointJsonTokens
    {
        public const string QuoteEndpointHeader = "Global Quote";

        public const string QuoteEndpointSymbolToken = "01. symbol";
        public const string QuoteEndpointOpenPriceToken = "02. open";
        public const string QuoteEndpointHighPriceToken = "03. high";
        public const string QuoteEndpointLowPriceToken = "04. low";
        public const string QuoteEndpointPriceToken = "05. price";
        public const string QuoteEndpointVolumeToken = "06. volume";
        public const string QuoteEndpointLatestTradingDayToken = "07. latest trading day";
        public const string QuoteEndpointPreviousClosePriceToken = "08. previous close";
        public const string QuoteEndpointChangePriceToken = "09. change";
        public const string QuoteEndpointChangePctToken = "10. change percent";
    }
}