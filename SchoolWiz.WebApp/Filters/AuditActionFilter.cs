﻿using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using SchoolWiz.Common.Models;
using SchoolWiz.Entity;
using SchoolWiz.WebApp.Models;
using SchoolWiz.WebApp.Models.Base;

namespace SchoolWiz.WebApp.Filters
{
    public class AuditActionFilter : ActionFilterAttribute
    {
        private readonly string[] _actions = { "Create", "Edit", "Assign", "Register", "AddAccountRates" };
        private readonly UserManager<ApplicationUser> _userManager;

        public AuditActionFilter(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.ActionConstraints != null && context.ActionDescriptor.ActionConstraints.Any())
            {
                if (context.ActionDescriptor.ActionConstraints[0] is HttpMethodActionConstraint methodConstraint && methodConstraint.HttpMethods.Contains(HttpMethod.Post.ToString().ToUpper()))
                {
                    var actionParameters = context.ActionArguments;
                    var actionName =
                        ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor)
                        .ActionName;
                    if (_actions.Contains(actionName))
                    {
                        var model = (AuditModelBase)actionParameters["model"];

                        if (model != null)
                        {
                            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                            switch (actionName)
                            {
                                case "Create":
                                case "Register":
                                    model.CreatedById = Guid.Parse(userId);
                                    model.CreatedDate = DateTime.Now;
                                    break;

                                case "Edit":
                                case "Assign":
                                case "AddAccountRates":
                                    model.ModifiedById = Guid.Parse(userId);
                                    model.ModifiedDate = DateTime.Now;

                                    break;
                            }
                        }
                    }
                }
            }

            base.OnActionExecuting(context);

        }
    }
}
