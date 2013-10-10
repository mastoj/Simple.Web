using System.Text.RegularExpressions;

namespace Simple.Web.Optimization
{
    public static class RegexHelpers
    {
        public static string RegexReplace(this string target, string pattern, string replacement)
        {
            return new Regex(pattern).Replace(target, replacement);            
        }
    }
}