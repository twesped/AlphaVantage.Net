using System;
using System.Collections.Generic;
using System.Text;

namespace Tw.AlphaVantage.Net.Stocks.TechIndicators
{
    public class TechIndicatorDataPoint
    {
        public DateTime Time { get; set; }

        public decimal Value { get; set; }
    }
}
