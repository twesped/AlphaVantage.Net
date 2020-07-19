using System;

namespace Tw.AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single element of a Stock End Point
    /// </summary>
    public class StockEndPoint
    {
        public string Symbol { get; set; }

        public DateTime Time {get; set;}

        public DateTime LastTradingDay { get; set; }

        public decimal Price { get; set; }

        public decimal OpeningPrice {get; set;}
        
        public decimal HighestPrice {get; set;}
        
        public decimal LowestPrice {get; set;}
        
        public long Volume {get; set;}

        public decimal ChangePct { get; set; }

        public decimal ChangeAbsolute { get; set; }
    }
}