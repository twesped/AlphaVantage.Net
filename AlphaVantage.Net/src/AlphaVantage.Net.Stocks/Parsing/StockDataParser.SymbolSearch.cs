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
        public ICollection<StockSymbolSearch> ParseSymbolSearch(JObject jObject)
        {
            if(jObject == null) throw new ArgumentNullException(nameof(jObject));

            try
            {
                var properties = jObject.Children().Select(ch => (JProperty) ch).ToArray();
                var stockQuotesJson = properties.FirstOrDefault(p => p.Name == SymbolSearchJsonTokens.SymbolSearchHeader);
                if (stockQuotesJson == null)
                    throw new StocksParsingException("Unable to parse Symbol Search data");

                var result = new List<StockSymbolSearch>();
                var contentDict = new Dictionary<string, string>();
                foreach (var quoteJson in stockQuotesJson.Value)
                {
                    var quoteProperties = quoteJson.Children().Select(q => (JProperty) q).ToArray();
                    foreach (var quoteProperty in quoteProperties)
                    {
                        contentDict.Add(quoteProperty.Name, quoteProperty.Value.ToString());
                    }

                    var item = ComposeSymbolSeachItem(contentDict);
                    contentDict.Clear();

                    result.Add(item);
                }

                return result;
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

        private StockSymbolSearch ComposeSymbolSeachItem(IDictionary<string, string> item)
        {
            var result = new StockSymbolSearch()
            {
                Symbol = item[SymbolSearchJsonTokens.SymbolSearchSymbolToken],
                Name = item[SymbolSearchJsonTokens.SymbolSearchNameToken],
                Region = item[SymbolSearchJsonTokens.SymbolSearchRegionToken],
                Type = item[SymbolSearchJsonTokens.SymbolSearchTypeToken],
                MarketOpen = TimeSpan.Parse(item[SymbolSearchJsonTokens.SymbolSearchMarketOpenToken]),
                MarketClose = TimeSpan.Parse(item[SymbolSearchJsonTokens.SymbolSearchMarketCloseToken]),                
                TimeZone = item[SymbolSearchJsonTokens.SymbolSearchTimeZoneToken],
                Currency = item[SymbolSearchJsonTokens.SymbolSearchCurrencyToken],
                MatchScore = Decimal.Parse(item[SymbolSearchJsonTokens.SymbolSearchMatchScoreToken], CultureInfo.InvariantCulture)
            };
                        
            return result;
        }
    }
}