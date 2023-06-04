using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomFilter.Utilities
{
    public class TrimStringPropertiesAttribute: ActionFilterAttribute
    {
        #region OLD
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var actionParams = context.ActionArguments;

        //    foreach (var actionParam in actionParams.Values)
        //    {
        //        if (actionParam == null) continue;

        //        var stringProperties = actionParam.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));

        //        foreach (var stringProperty in stringProperties)
        //        {
        //            var currentValue = stringProperty.GetValue(actionParam) as string;
        //            if (!string.IsNullOrEmpty(currentValue))
        //            {
        //                stringProperty.SetValue(actionParam, currentValue.Trim());
        //            }
        //        }

        //        var nestedModels = actionParam.GetType().GetProperties().Where(p => p.PropertyType.Name.Contains("NestedModel"));

        //        foreach (var nestedModel in nestedModels)
        //        {
        //            if (nestedModel.GetValue(actionParam) != null)
        //            {
        //                var nestedModelStringProperties = nestedModel.PropertyType.GetProperties().Where(p => p.PropertyType == typeof(string));

        //                foreach (var nestedModelStringProperty in nestedModelStringProperties)
        //                {
        //                    var currentValue = nestedModelStringProperty.GetValue(nestedModel.GetValue(actionParam)) as string;
        //                    if (!string.IsNullOrEmpty(currentValue))
        //                    {
        //                        nestedModelStringProperty.SetValue(nestedModel.GetValue(actionParam), currentValue.Trim());
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var properties = context.ActionArguments.Values.OfType<object>()
                                            .SelectMany(arg => arg.GetType().GetProperties());

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(string))
                {
                    var value = (string)prop.GetValue(context.ActionArguments[prop.Name]);
                    if (!string.IsNullOrEmpty(value))
                    {
                        prop.SetValue(context.ActionArguments[prop.Name], value.Trim());
                    }
                }
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(object))
                {
                    var nestedObject = prop.GetValue(context.ActionArguments[prop.Name]);
                    if (nestedObject != null)
                    {
                        TrimStrings(nestedObject);
                    }
                }
            }

            base.OnActionExecuting(context);
        }

        private void TrimStrings(object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(string))
                {
                    var value = (string)prop.GetValue(obj);
                    if (!string.IsNullOrEmpty(value))
                    {
                        prop.SetValue(obj, value.Trim());
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
