using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolWiz.Entity;

namespace SchoolWiz.WebApp.Controllers
{
    public class ApplicationBaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationBaseController( UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId != null ? Guid.Parse(userId) : Guid.Empty;
        }

        public string GetUserName(Guid userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            return user != null ? $@"{user.FirstName.Trim()} {user.LastName.Trim()}" : string.Empty;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);
                    ViewData.Add("FullName", "Anonymous");
                    if (user != null)
                    {
                        var fullName = $@"{user.FirstName.Trim()}  {user.LastName.Trim()}";
                        ViewData["FullName"] =  fullName;
                    }
                }
            }
            base.OnActionExecuted(filterContext);
        }

        protected void SetTempDataForToastNotification(bool displayToast, string toastType, string toastTitle, string toastMessage)
        {
            TempData["DisplayToast"] = true;
            TempData["ToastType"] = toastType;
            TempData["ToastTitle"] = toastTitle;
            TempData["ToastMessage"] = toastMessage;
        }
    }
}
