using System.Collections.Generic;
using Tw.AlphaVantage.Net.Core;
using Tw.AlphaVantage.Net.Core.Validation;

namespace Tw.AlphaVantage.Net.Stocks.Validation
{
    internal class StocksApiCallValidator : IApiCallValidator
    {
        public ValidationResult Validate(ApiFunction function, IDictionary<string, string> query)
        {
            throw new System.NotImplementedException();
        }
    }
}