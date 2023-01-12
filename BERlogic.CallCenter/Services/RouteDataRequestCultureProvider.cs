using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace BERlogic.CallCenter.Services
{
    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public int IndexOfCulture;
        public int IndexOfUiCulture;

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string culture = null;
            string uiCulture = null;

            var twoLetterCultureName = httpContext.Request.Path.Value.Split('/')[IndexOfCulture];
            var twoLetterUiCultureName = httpContext.Request.Path.Value.Split('/')[IndexOfUiCulture];

            if (twoLetterCultureName == "de")
                culture = "de-DE";
            else if (twoLetterCultureName == "en")
                culture = uiCulture = "en-US";
            else if (twoLetterCultureName == "ru")
                culture = uiCulture = "ru-RU";

            if (twoLetterUiCultureName == "de")
                culture = "de-DE";
            else if (twoLetterUiCultureName == "en")
                culture = uiCulture = "en-US";
            else if (twoLetterUiCultureName == "ru")
                culture = uiCulture = "ru-RU";

            if (culture == null && uiCulture == null)
                return NullProviderCultureResult;
       
            if (culture != null && uiCulture == null)
                uiCulture = culture;

            if (culture == null && uiCulture != null)
                culture = uiCulture;

            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult(providerResultCulture);
        }
    }
}
