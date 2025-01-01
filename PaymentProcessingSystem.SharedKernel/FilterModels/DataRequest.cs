using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace PaymentProcessingSystem.SharedKernel.FilterModels;

public class DataRequest<T>
{

    public string Query { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<T, bool>> Where { get; set; }
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    /// <summary>
    /// Check the query string if contains number
    /// </summary>
    /// <returns></returns>
    public bool QueryContainsNum()
    {
        return Query == null ? false : Regex.IsMatch(Query, @"\d", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
            TimeSpan.FromMilliseconds(100));
    }
    /// <summary>
    /// Check the query string if Number only
    /// </summary>
    /// <returns></returns>
    public bool QueryNumberOnly()
    {
        return Query == null ? false : Regex.IsMatch(Query, "^[0-9]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
            TimeSpan.FromMilliseconds(100));
    }
    /// <summary>
    /// Check the query string if letters only
    /// </summary>
    /// <returns></returns>
    public bool Querylettersonly()
    {
        return Query == null ? false : Regex.IsMatch(Query, @"^[a-zA-Z]+$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
            TimeSpan.FromMilliseconds(100));
    }
    /// <summary>
    /// Check the query string if below of max
    /// </summary>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public bool QueryLength(int maxLength)
    {
        return Query == null ? false : Query.ToString().Length <= maxLength ? true : false;
    }
    public bool QuerybetweenLength(int minLength, int maxLength)
    {
        return Query == null ? false : Query.ToString().Length > minLength && Query.ToString().Length < maxLength ? true : false;
    }
    public bool QueryIsContains(string contains)
    {
        return Query == null ? false : Query.ToString().Contains(contains) ? true : false;
    }
}
