using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tw.AlphaVantage.Net.Core;
using Tw.AlphaVantage.Net.Stocks.BatchQuotes;
using Tw.AlphaVantage.Net.Stocks.Parsing;
using Tw.AlphaVantage.Net.Stocks.TimeSeries;
using Tw.AlphaVantage.Net.Stocks.TechIndicators;
using Tw.AlphaVantage.Net.Stocks.Utils;


namespace Tw.AlphaVantage.Net.Stocks
{
    /// <summary>
    /// Client for Alpha Vantage API (stock data only)
    /// </summary>
    public class AlphaVantageTechIndicatorsClient
    {
        private readonly string _apiKey;
        private readonly AlphaVantageCoreClient _coreClient;
        
        public AlphaVantageTechIndicatorsClient(string apiKey)
        {
            if(string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            
            _apiKey = apiKey;
            _coreClient = new AlphaVantageCoreClient();
        }

        public async Task<TechIndicatorSeries> RequestDailyTechIndicatorsAsync(
            ApiFunction function,  // the technical indicator function
            string symbol,            
            int TimePeriod,
            Interval interval = Interval.Daily,
            SeriesType seriesType = SeriesType.Close)
        {
            if (TimePeriod <= 0) throw new ArgumentException("TimePeriod must be a positive integer");

            var query = new Dictionary<string, string>()
            {
                {StockApiQueryVars.Symbol, symbol},
                {StockApiQueryVars.TimePeriod, TimePeriod.ToString()},
                {StockApiQueryVars.Interval, interval.ConvertToString()},
                {StockApiQueryVars.SeriesType, seriesType.ConvertToString()}
            };
            
            return await RequestTechIndicatorsAsync(function, query);
        }

        /// <summary>
        /// BAse method to get Technical Indicators of all types
        /// </summary>
        /// <param name="function"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<TechIndicatorSeries> RequestTechIndicatorsAsync(
            ApiFunction function, 
            Dictionary<string, string> query)
        {
            var jObject = await _coreClient.RequestApiAsync(_apiKey, function, query);
            var parser = new TechIndicatorDataParser(function);
            var techIndicators = parser.ParseTechIndicatorSeries(jObject);
            
            return techIndicators;
        }
    }
}