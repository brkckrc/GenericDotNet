using CustomFilter.Models;
using CustomFilter.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CustomFilter.Controllers
{
    [TrimStringProperties]
    public class TestController : Controller
    {
        
        public IActionResult MyTestAction(TestModel testModel)
        {
            return Content($"{testModel.StringProp} \n" +
               $"{testModel?.NestedModel?.NestedStringProp} \n" +
               $"{testModel?.NestedModel?.NestedModel2?.StringProperty2}");
        }
    }
}
