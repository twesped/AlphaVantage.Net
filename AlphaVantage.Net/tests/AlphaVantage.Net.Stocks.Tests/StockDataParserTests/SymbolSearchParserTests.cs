using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tw.AlphaVantage.Net.Stocks.Parsing;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests.StockDataParserTests
{
    public class SymbolSearchParserTests
    {
        [Fact]
        public void SymbolSearch_ParsingTest()
        {
            var json = File.ReadAllText("Data/symbol-search.json");
            var jObject = (JObject) JsonConvert.DeserializeObject(json);

            var parser = new StockDataParser();
            var result = parser.ParseSymbolSearch(jObject);
            
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.True(
                result.Any(r => r.Symbol == "BA") &&
                result.Any(r => r.Symbol == "BAC") && 
                result.Any(r => r.Symbol == "BABA"));
        }
    }
}