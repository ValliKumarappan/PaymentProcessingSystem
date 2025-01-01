using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace PaymentProcessingSystem.Core.Helpers
{
    public static class ReportExtensions
    {
        public static int GetPageNumber(int pageNumber, int pageSize)
        {
            return (pageNumber - 1) * pageSize;
        }

        public static double GetPercentage(double number1, double number2)
        {
            double percentage = 0;
            if (!(number1 == 0 || number2 == 0))
                percentage = number1 < number2 ? Math.Round(number1 / number2 * 100, 2) : Math.Round(number2 / number1 * 100, 2);
            return percentage;
        }

        private static decimal GetPercentage(decimal number1, decimal number2)
        {
            decimal percentage = 0;
            if (number1 == 0 || number2 == 0) return percentage;
            percentage = number1 < number2 ? Math.Round(number1 / number2 * 100, 2) : Math.Round(number2 / number1 * 100, 2);
            return percentage;
        }

        private static decimal GetPercentage100(decimal number1, decimal number2)
        {
            decimal percentage = 0;
            if (number1 == 0 || number2 == 0) return 100 - percentage;
            percentage = number1 < number2 ? Math.Round(number1 / number2 * 100, 2) : Math.Round(number2 / number1 * 100, 2);
            return 100 - percentage;
        }

        public static decimal GetPercentage(decimal number1, decimal number2, int decimals)
        {
            decimal percentage = 0;
            if (number1 == 0 || number2 == 0) return percentage;
            percentage = number1 < number2 ? Math.Round(number1 / number2 * 100, decimals) : Math.Round(number2 / number1 * 100, decimals);
            return percentage;
        }

        public static decimal Getmultiply(decimal number1, decimal number2)
        {
            decimal valueamount = 0;
            if (number1 == 0 || number2 == 0) return valueamount;
            valueamount = number1 < number2 ? Math.Round(number1 / number2 * 100, 2) : Math.Round(number2 / number1 * 100, 2);
            return valueamount;
        }

        public static IEnumerable<DateTime> GetDateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1).Select(d => fromDate.AddDays(d));
        }

        public static IEnumerable<DateTime> GetMonthDateRange(DateTime startingDate, DateTime endingDate)
        {
            while (startingDate <= endingDate)
            {
                yield return startingDate;
                startingDate = startingDate.AddMonths(1);
            }
        }

        public static Expression<Func<T, bool>> PredicateBuilder<T>(Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static DateTime GetPreviousWeekDay(DateTime currentDate, DayOfWeek dow)
        {
            int currentDay = (int)currentDate.DayOfWeek, gotoDay = (int)dow;
            return currentDate.AddDays(-7).AddDays(gotoDay - currentDay);
        }

        public static DateTime StartOfDay(DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }

        public static List<long> StringtolongList(string obj)
        {
            return string.IsNullOrEmpty(obj) ? [] : obj.ToLower().Split(',').Select(o => o.ToLong()).ToList();
        }

        public static List<int> StringtoIntList(string obj)
        {
            return string.IsNullOrEmpty(obj) ? [] : obj.ToLower().Split(',').Select(o => o.ToSafeInt()).ToList();
        }

        public static List<float> StringtoFloatList(string obj)
        {
            return string.IsNullOrEmpty(obj) ? [] : obj.ToLower().Split(',').Select(o => o.ToSafeFloat()).ToList();
        }

        public static List<bool> StringtoBoolList(string obj)
        {
            return string.IsNullOrEmpty(obj) ? [] : obj.ToLower().Split(',').Select(o => o.ToBoolen()).ToList();
        }

        public static List<string> StringtoList(string obj)
        {
            return string.IsNullOrEmpty(obj) ? [] : obj.ToLower().Split(',').ToList();
        }

        public static string GetDifPercent(decimal amount, decimal prevamount)
        {
            return prevamount == 0 ? "" : amount != 0 && amount == prevamount ? "100%"
                : amount > prevamount ? $"{GetPercentage(amount, prevamount).ToString(CultureInfo.InvariantCulture)}%"
                : amount == 0 ? "-100%" : $"-{GetPercentage(prevamount, amount).ToString(CultureInfo.InvariantCulture)}%";
        }

        public static string GetDifDiscountPercent(decimal amount, decimal amount2)
        {
            return amount2 == 0 ? "0%" : amount != 0 && amount == amount2 ? "100%"
                : amount > amount2 ? $"{GetPercentage(amount, amount2).ToString(CultureInfo.InvariantCulture)}%"
                : amount == 0 ? "-100%" : $"-{GetPercentage(amount2, amount).ToString(CultureInfo.InvariantCulture)}%";
        }

        public static string GetDiscountPercent(decimal amount, decimal amount2)
        {
            return amount2 == 0 ? "0%" : amount != 0 && amount == amount2 ? "100%"
                : amount > amount2 ? $"{GetPercentage100(amount, amount2).ToString(CultureInfo.InvariantCulture)}%"
                : amount == 0 ? "-100%" : $"-{GetPercentage100(amount2, amount).ToString(CultureInfo.InvariantCulture)}%";
        }

         public static string DeviceToRename(this string device)
        {
            return device switch
            {
                "IOSWeb" => "IOS Web",
                "AgentBooking" => "Agent Booking",
                "AndroidWeb" => "Android Web",
                _ => device
            };
        }

        #region  helper

        private static int GetNumberOfNights(DateTime startDate, DateTime endDate)
        {
            var totalNights = (int)(startDate - endDate).TotalDays;
            return totalNights > 1 ? totalNights : 1;
        }

        public static bool IsDigitOnly(string text)
        {
            try
            {
                return text.All(char.IsDigit);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsTextEnglish(string text)
        {
            try
            {
                var isEnglishOnly = Regex.IsMatch(text, "^[a-zA-Z0-9. -_?]*$",
                    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
                return isEnglishOnly;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<string> StringToList(string str)
        {
            var res = new List<string>();
            return !string.IsNullOrEmpty(str) ? str.Split(',').Select(o => o.ToString()).ToList() : res;
        }
        #endregion
    }
}
