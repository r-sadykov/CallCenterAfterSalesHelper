#region Using

using BERlogic.CallCenter.Controllers;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BERlogic.CallCenter.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(action: nameof(AccountController.ConfirmEmail), controller: "Account", values: new
            {
                userId,
                code
            }, protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme,string htmlDomain="")
        {
            return !string.IsNullOrWhiteSpace(htmlDomain)?urlHelper.Action(action: nameof(AccountController.ResetPassword), controller: "Account", values: new
            {
                userId,
                code
            }, protocol: scheme, host: htmlDomain) : urlHelper.Action(action: nameof(AccountController.ResetPassword), controller: "Account", values: new
            {
                userId,
                code
            }, protocol: scheme);
        }
    }
}
