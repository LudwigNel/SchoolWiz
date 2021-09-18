﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Html;
using SchoolWiz.Common.Extensions;
using SchoolWiz.WebApp.Models;

namespace SchoolWiz.WebApp.Extensions
{
    public static class IdentityExtensions
    {
        [DebuggerStepThrough]
        private static IEnumerable<Claim> GetClaims(this IIdentity identity, string claimType)
        {
            var ci = identity as ClaimsIdentity;

            return (ci?.Claims.Where(c => c.Type.Equals(claimType, StringComparison.InvariantCultureIgnoreCase)) ?? Enumerable.Empty<Claim>()).ToList();
        }

        [DebuggerStepThrough]
        public static ICollection<string> GetClaimsValues(this IIdentity identity, string claimType) => identity.GetClaims(claimType).Select(c => c.Value).ToList();

        [DebuggerStepThrough]
        public static bool HasRole(this IPrincipal principal, params string[] roles)
        {
            var claims = principal?.Identity?.GetClaimsValues(ClaimTypes.Role);

            return claims?.Any() == true && claims.Intersect(roles ?? new string[] { }).Any();
        }

        [DebuggerStepThrough]
        public static IEnumerable<ListItem> AuthorizeFor(this IEnumerable<ListItem> source, ClaimsPrincipal identity)
            => source.Where(x => x.Roles.IsNullOrEmpty() || (x.Roles.HasItems() && identity.HasRole(x.Roles))).ToList();

        [DebuggerStepThrough]
        public static HtmlString AsRaw(this string value) => new HtmlString(value);
    }
}
