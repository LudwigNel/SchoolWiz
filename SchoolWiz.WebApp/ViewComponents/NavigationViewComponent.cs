using Microsoft.AspNetCore.Mvc;
using SchoolWiz.WebApp.Models;

namespace SchoolWiz.WebApp.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var items = NavigationModel.Seed;

            return View(items);
        }
    }
}
