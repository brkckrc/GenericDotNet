using System.Text.RegularExpressions;

namespace CustomFilter.Utilities
{
    public static class Extensions
    {
        public static string MyTrim(this String myString)
        {            
            return Regex.Replace(myString, @"\s+", " ").Trim();
        }
    }
}
