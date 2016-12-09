using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace deprosa.API.Authenticator
{
    public class EnsureLoggedInAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {

            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            }
        }
    }

    public class EnsureCanSellAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated && Thread.CurrentPrincipal.IsInRole("Seller"))
            {
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}