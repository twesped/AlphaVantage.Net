// ReSharper disable InconsistentNaming
namespace Tw.AlphaVantage.Net.Stocks.Parsing.JsonTokens
{
    internal static class TechIndicatorJsonTokens
    {
        public const string TechIndicatorHeader = "Technical Analysis";
        
        public const string SymbolToken = "1: Symbol";
        public const string IndicatorToken = "2: Indicator";
        public const string LastRefreshedToken = "3: Last Refreshed";
        public const string IntervalToken = "4: Interval";
        public const string TimePeriodToken = "5: Time Period";
        public const string SeriesTypeToken = "6: Series Type";
        public const string TimeZoneToken = "7: Time Zone";

        public const string OneMinToken = "1min";
        public const string FiveMinToken = "5min";
        public const string FifteenMinToken = "15min";
        public const string ThirtyMinToken = "30min";
        public const string SixtyMinToken = "60min";
        public const string DailyToken = "daily";
        public const string WeeklyToken = "weekly";
        public const string MonthlyToken = "monthly";
    }
}