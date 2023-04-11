using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomFilter.Utilities
{
    public class TrimStringPropertiesAttribute: ActionFilterAttribute
    {

        #region OLD OnActionExecuting Method
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

            //Reflection Example
            //// TestModel içindeki StringProp property'si için değerini alalım
            //      TestModel model = new TestModel();
            //      model.StringProp = "Hello World";
            //      PropertyInfo prop = model.GetType().GetProperty("StringProp");
            //      string value = (string)prop.GetValue(model);
            //      Console.WriteLine(value); // Output: Hello World

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(string))
                {
                    var modelName = context.ActionArguments.ToList()[0].Key;
                    var myProp = context.ActionArguments[modelName].GetType().GetProperty(prop.Name);
                    var myValue = context.ActionArguments.ToList()[0].Value;
                    var result = (string)myProp.GetValue(myValue);

                    if (!string.IsNullOrEmpty(result))
                    {
                        prop.SetValue(context.ActionArguments[modelName], result.MyTrim());
                    }
                }
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(object))
                {
                    var modelName = context.ActionArguments.ToList()[0].Key;
                    var test = context.ActionArguments[modelName].GetType().GetProperty(prop.Name);
                    var myValue = context.ActionArguments.ToList()[0].Value;
                    var result = test.GetValue(myValue);
                    if (result != null)
                    {
                        Helpers.TrimStrings(result);
                    }
                }
            }

            base.OnActionExecuting(context);
        }


    }
}
