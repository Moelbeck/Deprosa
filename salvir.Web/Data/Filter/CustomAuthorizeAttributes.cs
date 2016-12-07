using System.Web.Mvc;
using System.Web.Routing;
using deprosa.Web.Model;

namespace deprosa.Filter
{


    public class EnsureLoggedInAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.Email) || CurrentUser.ID < 0)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }

    public class EnsureCanSellAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!CurrentUser.IsCompanyValidated)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }

}
