using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tw.AlphaVantage.Net.Stocks.BatchQuotes;
using Tw.AlphaVantage.Net.Stocks.Parsing.Exceptions;
using Tw.AlphaVantage.Net.Stocks.Parsing.JsonTokens;
using Newtonsoft.Json.Linq;
using Tw.AlphaVantage.Net.Stocks.TimeSeries;

namespace Tw.AlphaVantage.Net.Stocks.Parsing
{
    internal partial class StockDataParser
    {
        public StockEndPoint ParseEndPoint(JObject jObject)
        {
            if(jObject == null) throw new ArgumentNullException(nameof(jObject));

            try
            {
                var properties = jObject.Children().Select(ch => (JProperty) ch).ToArray();
                var stockQuotesJson = properties.FirstOrDefault(p => p.Name == QuoteEndpointJsonTokens.QuoteEndpointHeader);
                if (stockQuotesJson == null)
                    throw new StocksParsingException("Unable to parse stock end point data");

                // Get all elements in set
                var elements = stockQuotesJson.Value.Select(q => (JProperty)q).ToArray();
                var contentDict = new Dictionary<string, string>();
                foreach (var quoteProperty in elements)
                {
                    contentDict.Add(quoteProperty.Name, quoteProperty.Value.ToString());
                }

                var quote = ComposeEndPoint(contentDict);
                return quote;
            }
            catch (StocksParsingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new StocksParsingException("Unable to parse data. See the inner exception for details", ex);
            }
        }

        private StockEndPoint ComposeEndPoint(IDictionary<string, string> stockEndPointContent)
        {
            var result = new StockEndPoint()
            {
                Symbol = stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointSymbolToken],
                Time = DateTime.Now,
                Price = Decimal.Parse(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointPriceToken], CultureInfo.InvariantCulture),
                OpeningPrice = Decimal.Parse(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointOpenPriceToken], CultureInfo.InvariantCulture),
                HighestPrice = Decimal.Parse(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointHighPriceToken], CultureInfo.InvariantCulture),
                LowestPrice = Decimal.Parse(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointLowPriceToken], CultureInfo.InvariantCulture),
                ChangeAbsolute = Decimal.Parse(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointChangePriceToken], CultureInfo.InvariantCulture),
                ChangePct = Utils.StockClientExtentions.FromPercentageString(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointChangePctToken]),
                LastTradingDay = DateTime.ParseExact(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointLatestTradingDayToken], "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
            
            if(Int64.TryParse(stockEndPointContent[QuoteEndpointJsonTokens.QuoteEndpointVolumeToken], out var volume))
                result.Volume = volume;

            return result;
        }
    }
}