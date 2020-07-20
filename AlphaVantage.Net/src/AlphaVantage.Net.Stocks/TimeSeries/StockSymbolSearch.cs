using System;

namespace Tw.AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single element of a Stock End Point
    /// </summary>
    public class StockSymbolSearch
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Type {get; set;}
        
        public string Region {get; set;}

        public TimeSpan MarketOpen { get; set; }

        public TimeSpan MarketClose { get; set; }

        public string TimeZone {get; set;}
        
        public string Currency {get; set;}

        public decimal MatchScore { get; set; }
    }
}