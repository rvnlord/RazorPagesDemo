using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace RazorPagesDemo
{
    public class EvenConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return values.ContainsKey("id") && int.TryParse(values["id"].ToString(), out var id) && id % 2 == 0;
        }
    }
}
