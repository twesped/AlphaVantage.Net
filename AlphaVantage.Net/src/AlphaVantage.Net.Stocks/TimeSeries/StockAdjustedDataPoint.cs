﻿using System;

namespace Tw.AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single adjusted element of time series
    /// </summary>
    public sealed class StockAdjustedDataPoint : StockDataPoint
    {
        public decimal AdjustedClosingPrice {get; set;}
        
        public decimal DividendAmount {get; set;}
        
        public decimal? SplitCoefficient { get; set; }
    }
}