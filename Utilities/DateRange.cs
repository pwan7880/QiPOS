using System;
using System.Data;
using System.Globalization;

namespace QiPOS
{
    public sealed class DateRange
    {
        public string CRDateRange { get; private set; }
        public string SQLDateRange { get; private set; }
        public DateTime fromDay { get; private set; }
        public DateTime toDay { get; private set; }

        public DateRange(string option) : this(option, DateTime.Now) { }

        public DateRange(string option, DateTime currentDay)
        {
            Calculate(option, currentDay);
        }

        private static string ToCRDate(DateTime date) =>
            $" = Date({date.Year},{date.Month:D2},{date.Day:D2})";

        private static string ToSQLDate(DateTime date) =>
            $" = '{date:yyyy-MM-dd}'";

        private static string ToCRRange(DateTime from, DateTime to) =>
            $"in Date({from.Year},{from.Month:D2},{from.Day:D2}) to Date({to.Year},{to.Month:D2},{to.Day:D2})";

        private static string ToSQLRange(DateTime from, DateTime to) =>
            $" BETWEEN '{from:yyyy-MM-dd}' and '{to:yyyy-MM-dd}'";

        public static DateTime LastDayOfLastQuarter(DateTime date)
        {
            int month = ((date.Month - 1) / 3) * 3 + 1;
            return new DateTime(date.Year, month, 1);
        }

        public static DateTime GetThisFinancialYear(DateTime date)
        {
            return date.Month > 6
                ? new DateTime(date.Year, 7, 1)
                : new DateTime(date.Year - 1, 7, 1);
        }

        public static int GetCurrentWeekNumber(DateTime date)
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            return dfi.Calendar.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        public static DateTime GetStartOfWeek(DateTime date)
        {
            var diff = (int)date.DayOfWeek - 1;
            return date.AddDays(-diff >= 0 ? -diff : -6); // Handle Sunday as start of last week
        }

        public static int GetSystemDayOfWeek(DateTime date)
        {
            return (int)date.DayOfWeek;
        }

        private void Calculate(string rangeStr, DateTime day)
        {
            string range = rangeStr.Trim().ToLowerInvariant();
            switch (range)
            {
                case "today":
                    SetToday(day);
                    break;
                case "this week":
                    SetThisWeek(day);
                    break;
                case "month to date":
                    SetThisMonth(day);
                    break;
                case "quarter to date":
                    SetThisQuarter(day);
                    break;
                case "year to date":
                    SetThisYear(day);
                    break;
                case "last week":
                    SetLastWeek(day);
                    break;
                case "last month":
                    SetLastMonth(day);
                    break;
                case "last quarter":
                    SetLastQuarter(day);
                    break;
                case "last year":
                    SetLastYear(day);
                    break;
                default:
                    SetToday(day);
                    break;
            }
        }

        private void SetLastYear(DateTime day)
        {
            toDay = GetThisFinancialYear(day).AddDays(-1);
            fromDay = GetThisFinancialYear(toDay);
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetLastQuarter(DateTime day)
        {
            toDay = LastDayOfLastQuarter(day).AddDays(-1);
            fromDay = LastDayOfLastQuarter(toDay);
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetLastMonth(DateTime day)
        {
            DateTime firstOfThisMonth = new DateTime(day.Year, day.Month, 1);
            toDay = firstOfThisMonth.AddDays(-1);
            fromDay = new DateTime(toDay.Year, toDay.Month, 1);
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetLastWeek(DateTime day)
        {
            int daysToLastSunday = (int)day.DayOfWeek;
            toDay = day.Date.AddDays(-daysToLastSunday);
            fromDay = toDay.AddDays(-6);
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetThisYear(DateTime day)
        {
            fromDay = GetThisFinancialYear(day);
            toDay = day;
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetThisQuarter(DateTime day)
        {
            fromDay = LastDayOfLastQuarter(day);
            toDay = day;
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetThisMonth(DateTime day)
        {
            fromDay = new DateTime(day.Year, day.Month, 1);
            toDay = day;
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetThisWeek(DateTime day)
        {
            int daysSinceMonday = ((int)day.DayOfWeek + 6) % 7;
            fromDay = day.Date.AddDays(-daysSinceMonday);
            toDay = day;
            CRDateRange = ToCRRange(fromDay, toDay);
            SQLDateRange = ToSQLRange(fromDay, toDay);
        }

        private void SetToday(DateTime day)
        {
            fromDay = toDay = day.Date;
            CRDateRange = ToCRDate(day);
            SQLDateRange = ToSQLDate(day);
        }
    }

    public static class WeekHelper
    {
        public static DataTable GetDayOfWeekTable(bool includeNullRow = false, Func<int, bool> filter = null)
        {
            var table = new DataTable();
            table.Columns.Add("dayofweek", typeof(int));
            table.Columns.Add("week_short", typeof(string));

            var days = new[]
            {
            (1, "Mon"),
            (2, "Tue"),
            (3, "Wed"),
            (4, "Thu"),
            (5, "Fri"),
            (6, "Sat"),
            (0, "Sun")
        };

            foreach (var (value, label) in days)
            {
                if (filter == null || filter(value))
                    table.Rows.Add(value, label);
            }

            if (includeNullRow)
                table.Rows.InsertAt(table.NewRow(), 0);

            return table;
        }
    }

}
