using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolWiz.Common.Models.StudentRegistration;

namespace SchoolWiz.WebApp.Filters
{
    public class ValidateAlternateGuardianFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.FirstOrDefault(p => p.Key == "model").Value is StudentRegistrationCreateViewModel model)
            {
                if (model.AlternateGuardian != null && model.AlternateGuardian.HasValue())
                {
                    if(string.IsNullOrEmpty(model.AlternateGuardian.IdentityNumber))
                        context.ModelState.AddModelError("AlternateGuardian.IdentityNumber", "The Identity Number field is required");

                    if(string.IsNullOrEmpty(model.AlternateGuardian.FirstName))
                        context.ModelState.AddModelError("AlternateGuardian.FirstName", "The First Name field is required");

                    if (string.IsNullOrEmpty(model.AlternateGuardian.LastName))
                        context.ModelState.AddModelError("AlternateGuardian.LastName", "The Last Name field is required");

                    if (model.AlternateGuardian.GuardianTypeId == Guid.Empty)
                        context.ModelState.AddModelError("AlternateGuardian.GuardianTypeId", "The Guardian Type field is required");

                    if (string.IsNullOrEmpty(model.AlternateGuardian.MobileNumber))
                        context.ModelState.AddModelError("AlternateGuardian.MobileNumber", "The Mobile Number field is required");

                    if (string.IsNullOrEmpty(model.AlternateGuardian.Email))
                        context.ModelState.AddModelError("AlternateGuardian.Email", "The Email field is required");
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
