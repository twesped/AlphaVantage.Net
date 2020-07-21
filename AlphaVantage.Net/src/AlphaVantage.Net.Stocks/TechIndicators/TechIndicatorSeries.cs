using System;
using System.Collections.Generic;
using Tw.AlphaVantage.Net.Stocks.TimeSeries;

namespace Tw.AlphaVantage.Net.Stocks.TechIndicators
{
    public class TechIndicatorSeries
    {
        public string Symbol { get; set; }

        public string Indicator { get; set; }

        public DateTime LastRefreshed { get; set; }

        public Interval Interval {get; set;}

        public int TimePeriod { get; set; }

        public string SeriesType { get; set; }

        public string TimeZone { get; set; }

        public ICollection<TechIndicatorDataPoint> DataPoints {get; set;}
    }
}