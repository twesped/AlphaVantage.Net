using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tw.AlphaVantage.Net.Stocks.BatchQuotes;
using Tw.AlphaVantage.Net.Stocks.Parsing.Exceptions;
using Tw.AlphaVantage.Net.Stocks.Parsing.JsonTokens;
using Newtonsoft.Json.Linq;
using Tw.AlphaVantage.Net.Stocks.TimeSeries;
using Tw.AlphaVantage.Net.Stocks.TechIndicators;
using Tw.AlphaVantage.Net.Core;

namespace Tw.AlphaVantage.Net.Stocks.Parsing
{
    internal class TechIndicatorDataParser
    {
        private const string TimeStampKey = "timestamp";

        private ApiFunction _function;

        public TechIndicatorDataParser(ApiFunction function)
        {
            _function = function;
        }

        public TechIndicatorSeries ParseTechIndicatorSeries(JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));

            try
            {
                var properties = jObject.Children().Select(ch => (JProperty)ch).ToArray();

                var metadataJson = properties.FirstOrDefault(p => p.Name == MetaDataJsonTokens.MetaDataHeader);
                var timeSeriesJson =
                    properties.FirstOrDefault(p => p.Name.Contains(TechIndicatorJsonTokens.TechIndicatorHeader));

                if (metadataJson == null || timeSeriesJson == null)
                    throw new StocksParsingException("Unable to parse Technical Indicator json");

                var result = new TechIndicatorSeries();

                EnrichWithMetadata(metadataJson, result);
                result.DataPoints = GetTechIndicatorDataPoints(timeSeriesJson);

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

        private void EnrichWithMetadata(JProperty metadataJson, TechIndicatorSeries timeSeries)
        {
            var metadatas = metadataJson.Children().Single();

            foreach (var metadataItem in metadatas)
            {
                var metadataProperty = (JProperty)metadataItem;                
                var metadataItemName = metadataProperty.Name;
                var metadataItemValue = metadataProperty.Value.ToString();

                switch(metadataItemName)
                {
                    case TechIndicatorJsonTokens.SymbolToken:
                        timeSeries.Symbol = metadataItemValue;
                        break;
                    case TechIndicatorJsonTokens.IndicatorToken:
                        timeSeries.Indicator = metadataItemValue;
                        break;
                    case TechIndicatorJsonTokens.IntervalToken:
                        timeSeries.Interval = GetInterval(metadataItemValue);
                        break;
                    case TechIndicatorJsonTokens.LastRefreshedToken:
                        timeSeries.LastRefreshed = DateTime.Parse(metadataItemValue);
                        break;
                    case TechIndicatorJsonTokens.TimePeriodToken:
                        timeSeries.TimePeriod = int.Parse(metadataItemValue, CultureInfo.InvariantCulture);
                        break;
                    case TechIndicatorJsonTokens.TimeZoneToken:
                        timeSeries.TimeZone = metadataItemValue;
                        break;
                }               
            }
        }

        private Interval GetInterval(string metadataValue)
        {
            if (metadataValue.Contains(TechIndicatorJsonTokens.OneMinToken))
                return Interval.OneMin;
            if (metadataValue.Contains(TechIndicatorJsonTokens.FiveMinToken))
                return Interval.FiveMin;
            if (metadataValue.Contains(TechIndicatorJsonTokens.FifteenMinToken))
                return Interval.FifteenMin;
            if (metadataValue.Contains(TechIndicatorJsonTokens.ThirtyMinToken))
                return Interval.ThirtyMin;
            if (metadataValue.Contains(TechIndicatorJsonTokens.SixtyMinToken))
                return Interval.SixtyMin;
            if (metadataValue.Contains(TechIndicatorJsonTokens.DailyToken))
                return Interval.Daily;
            if (metadataValue.Contains(TechIndicatorJsonTokens.WeeklyToken))
                return Interval.Weekly;
            if (metadataValue.Contains(TechIndicatorJsonTokens.MonthlyToken))
                return Interval.Monthly;

            throw new StocksParsingException("Unable to determine time-series type");
        }

        private ICollection<TechIndicatorDataPoint> GetTechIndicatorDataPoints(JProperty timeSeriesJson)
        {
            var result = new List<TechIndicatorDataPoint>();

            var IndicatorsContent = timeSeriesJson.Children().Single();
            var contentDict = new Dictionary<string, string>();
            foreach (var dataPointJson in IndicatorsContent)
            {
                var dataPointJsonProperty = dataPointJson as JProperty;
                if (dataPointJsonProperty == null)
                    throw new StocksParsingException("Unable to parse indicator series");

                contentDict.Add(TimeStampKey, dataPointJsonProperty.Name);

                var dataPointContent = dataPointJsonProperty.Single();
                foreach (var field in dataPointContent)
                {
                    var property = (JProperty)field;
                    contentDict.Add(property.Name, property.Value.ToString());
                }

                var dataPoint = ComposeDataPoint(contentDict);

                result.Add(dataPoint);
                contentDict.Clear();
            }

            return result;
        }

        private TechIndicatorDataPoint ComposeDataPoint(Dictionary<string, string> dataPointContent)
        {
            var isAdjusted = dataPointContent.Count > 6;

            var dataPoint = new TechIndicatorDataPoint();

            dataPoint.Time = DateTime.Parse(dataPointContent[TimeStampKey]);
            dataPoint.Value = Decimal.Parse(dataPointContent[_function.ToString()], CultureInfo.InvariantCulture);
           
            return dataPoint;
        }
    }
}