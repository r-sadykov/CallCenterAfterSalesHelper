using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BERlogic.CallCenter.Services
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {

            if (!values.ContainsKey("lang"))
                return false;

            var culture = values["lang"].ToString();
            return culture == "en" || culture == "de" || culture == "ru";
        }
    }
}
