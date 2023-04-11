using CustomFilter.Models;
using CustomFilter.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CustomFilter.Controllers
{
    public class TestController : Controller
    {
        [TrimStringProperties]
        public IActionResult MyTestAction(TestModel testModel)
        {
            return Content($"{testModel.StringProp} \n" +
               $"{testModel.NestedModel.NestedStringProp} \n" +
               $"{testModel.NestedModel.NestedModel2.StringProperty2}");
        }
    }
}
