using System;
using Tw.AlphaVantage.Net.Core.Exceptions;

namespace Tw.AlphaVantage.Net.Stocks.Parsing.Exceptions
{
    public class StocksParsingException : AlphaVantageException
    {
        public StocksParsingException(string message) : base(message)
        {
        }

        public StocksParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}