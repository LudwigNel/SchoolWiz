using Microsoft.AspNetCore.Mvc;

namespace SchoolWiz.WebApp.Controllers
{
    public class FormController : Controller
    {
        public IActionResult BasicInputs() => View();
        public IActionResult CheckboxRadio() => View();
        public IActionResult InputGroups() => View();
        public IActionResult Validation() => View();
    }
}
