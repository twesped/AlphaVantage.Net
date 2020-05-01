﻿using System;
using System.Collections.Generic;

namespace Tw.AlphaVantage.Net.Stocks.TimeSeries
{
    public class StockTimeSeries
    {
        public TimeSeriesType Type {get; set;}
                
        public DateTime LastRefreshed {get; set;}
        
        public string Symbol {get; set;}
        
        public bool IsAdjusted {get; set;}
        
        public ICollection<StockDataPoint> DataPoints {get; set;}
    }
}