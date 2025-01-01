using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingSystem.Core.Helpers
{
    public static class ObjectExtensions
    {
        public static long ToLong(this object input)
        {
            long result = 0L;
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = Convert.ToInt64(input);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static double ToDouble(this object input)
        {
            double result = 0.0;
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = Convert.ToDouble(input);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static int ToInt(this object input)
        {
            int result = 0;
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = Convert.ToInt32(input);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static short ToShort(this object input)
        {
            short result = 0;
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = Convert.ToInt16(input);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static char ToChar(this object input)
        {
            char result = '\0';
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = Convert.ToChar(input);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static bool ToBoolen(this object input)
        {
            bool result = false;
            if (input == null || input == DBNull.Value)
            {
                return false;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = Convert.ToBoolean(input);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static string DecimalToN2(this decimal? input)
        {
            return input?.ToString("N2") ?? "";
        }

        public static decimal ToDecimal(this object input)
        {
            decimal result = 0.0m;
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = Convert.ToDecimal(input);
                    return result;
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static float ToFloat(this object input)
        {
            float result = 0f;
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = float.Parse(input.ToSafeString(), CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static string ToSafeString(this object input)
        {
            return (input ?? string.Empty).ToString();
        }

        public static int ToSafeInt(this object input)
        {
            return (input ?? ((object)0)).ToInt();
        }

        public static long ToSafeLong(this object input)
        {
            return (input ?? ((object)0)).ToLong();
        }

        public static double ToSafeDouble(this object input)
        {
            return (input ?? ((object)0)).ToDouble();
        }

        public static decimal ToSafeDecimal(this object input)
        {
            return (input ?? ((object)0)).ToDecimal();
        }

        public static float ToSafeFloat(this object input)
        {
            return (input ?? ((object)0)).ToFloat();
        }

        public static string SafeSubstring(this string value, int startIndex, int length)
        {
            return new string((value ?? string.Empty).Skip(startIndex).Take(length).ToArray());
        }

        public static string SafeRemove(this string value, int startIndex, int length)
        {
            if (startIndex < 0 || length < 0)
            {
                return value;
            }

            return (value ?? string.Empty).Remove(startIndex, length);
        }

        public static string SafeReplace(this string value, string oldChar, string newChar)
        {
            if (string.IsNullOrEmpty(oldChar))
            {
                return value;
            }

            return (value ?? string.Empty).Replace(oldChar, newChar);
        }

        public static DateTime ToDateTime(this object input)
        {
            DateTime result = DateTime.MinValue;
            string[] formats = new string[14]
            {
            "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd", "yyyy-MM-ddTH:mm:ss", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy", "MM/dd/yyyy HH:mm:ss", "MM/dd/yyyy", "dd MMM yyyy", "ddd, MMM dd, yyyy", "ddd, MMM d, yyyy",
            "dd MMM yyyy, ddd", "dd/MM/yyyy HH:mm:ss tt", "dd/M/yyyy HH:mm:ss", "dd/MMM/yyyy hh:mm:ss"
            };
            if (input == null || input == DBNull.Value)
            {
                return result;
            }

            try
            {
                if (string.Empty != input.ToString())
                {
                    result = DateTime.ParseExact(input.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        public static string ToJsonFormatter(this string json)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                bool flag = false;
                bool flag2 = false;
                int num = 0;
                int num2 = 0;
                if (string.IsNullOrEmpty(json))
                {
                    return string.Empty;
                }

                json = json.Replace(Environment.NewLine, "").Replace("\t", "");
                string text = json;
                foreach (char c in text)
                {
                    switch (c)
                    {
                        case '"':
                            if (!flag2)
                            {
                                flag = !flag;
                            }

                            break;
                        case '\'':
                            if (flag)
                            {
                                flag2 = !flag2;
                            }

                            break;
                    }

                    if (flag)
                    {
                        stringBuilder.Append(c);
                        continue;
                    }

                    switch (c)
                    {
                        case '[':
                        case '{':
                            stringBuilder.Append(c);
                            stringBuilder.Append(Environment.NewLine);
                            stringBuilder.Append(new string(' ', ++num * 4));
                            break;
                        case ']':
                        case '}':
                            stringBuilder.Append(Environment.NewLine);
                            stringBuilder.Append(new string(' ', --num * 4));
                            stringBuilder.Append(c);
                            break;
                        case ',':
                            stringBuilder.Append(c);
                            stringBuilder.Append(Environment.NewLine);
                            stringBuilder.Append(new string(' ', num * 4));
                            break;
                        case ':':
                            stringBuilder.Append(c);
                            stringBuilder.Append(' ');
                            break;
                        default:
                            if (c != ' ')
                            {
                                stringBuilder.Append(c);
                            }

                            break;
                    }

                    num2++;
                }

                return stringBuilder.ToString().Trim();
            }
            catch (Exception)
            {
                return json;
            }
        }

        public static string ToDateString(this DateTime input)
        {
            string result = string.Empty;
            try
            {
                result = input.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static string ToDateTimString(this DateTime input)
        {
            string result = string.Empty;
            try
            {
                result = input.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static bool IsNull(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static object ToDbDateTime(this DateTime input)
        {
            if (input == DateTime.MinValue)
            {
                return null;
            }

            return input;
        }

        public static object ToDbBool(this bool? input)
        {
            if (!input.HasValue)
            {
                return DBNull.Value;
            }

            return input;
        }

        public static object ToDbBool(this bool input)
        {
            if (!input)
            {
                return DBNull.Value;
            }

            return true;
        }

        public static bool IsNullOrEmpty<T, U>(this IDictionary<T, U> dictionary)
        {
            if (dictionary != null)
            {
                return dictionary.Count < 1;
            }

            return true;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list != null)
            {
                return list.Count < 1;
            }

            return true;
        }

        public static string CheckNullAndReturnString(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNull(this object targetObject)
        {
            if (targetObject != null)
            {
                return false;
            }

            return true;
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return from x in items.GroupBy(property)
                   select x.First();
        }

        public static string ToSabreDateTime(this DateTime input)
        {
            string result = string.Empty;
            try
            {
                result = input.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static string ToJsonString(this object input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
