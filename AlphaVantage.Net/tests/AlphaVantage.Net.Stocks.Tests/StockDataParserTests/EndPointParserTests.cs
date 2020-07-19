using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tw.AlphaVantage.Net.Stocks.Parsing;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests.StockDataParserTests
{
    public class EndPointParserTests
    {
        [Fact]
        public void EndPoint_ParsingTest()
        {
            var json = File.ReadAllText("Data/endpoint-quote.json");
            var jObject = (JObject) JsonConvert.DeserializeObject(json);

            var parser = new StockDataParser();
            var result = parser.ParseEndPoint(jObject);
            
            Assert.NotNull(result);
            Assert.True(
                result.Symbol == "IBM" &&
                result.Price == 125.11m);
        }
    }
}