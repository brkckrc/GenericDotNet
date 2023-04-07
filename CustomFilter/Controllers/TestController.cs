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
            
            return Content($".{ testModel.StringProp} {testModel.NestedModel.NestedStringProp}.");
        }
    }
}
