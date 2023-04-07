using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomFilter.Utilities
{
    public class TrimStringPropertiesAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionParams = context.ActionArguments;

            foreach (var actionParam in actionParams.Values)
            {
                if (actionParam == null) continue;

                var stringProperties = actionParam.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));

                foreach (var stringProperty in stringProperties)
                {
                    var currentValue = stringProperty.GetValue(actionParam) as string;
                    if (!string.IsNullOrEmpty(currentValue))
                    {
                        stringProperty.SetValue(actionParam, currentValue.Trim());
                    }
                }

                var nestedModels = actionParam.GetType().GetProperties().Where(p => p.PropertyType.Name.Contains("NestedModel"));

                foreach (var nestedModel in nestedModels)
                {
                    if (nestedModel.GetValue(actionParam) != null)
                    {
                        var nestedModelStringProperties = nestedModel.PropertyType.GetProperties().Where(p => p.PropertyType == typeof(string));

                        foreach (var nestedModelStringProperty in nestedModelStringProperties)
                        {
                            var currentValue = nestedModelStringProperty.GetValue(nestedModel.GetValue(actionParam)) as string;
                            if (!string.IsNullOrEmpty(currentValue))
                            {
                                nestedModelStringProperty.SetValue(nestedModel.GetValue(actionParam), currentValue.Trim());
                            }
                        }
                    }
                }
            }
        }
    }
}
