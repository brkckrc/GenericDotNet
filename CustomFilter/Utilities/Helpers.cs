using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomFilter.Utilities
{
    public class Helpers
    {
        public static void TrimStrings(object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(string))
                {
                    var value = (string)prop.GetValue(obj);
                    if (!string.IsNullOrEmpty(value))
                    {
                        //prop.SetValue(obj, value.Trim());
                        prop.SetValue(obj, value.MyTrim());
                    }
                    else
                    {
                        prop.SetValue(obj, "NULL");
                    }
                }
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(object))
                {
                    var nestedObject = prop.GetValue(obj);
                    if (nestedObject != null)
                    {
                        TrimStrings(nestedObject);
                    }
                }
            }
        }
    }
}
