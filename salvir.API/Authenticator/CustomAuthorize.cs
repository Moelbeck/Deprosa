using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace salvir.API.Authenticator
{
    public class EnsureLoggedInAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                //actionContext.Response = new HttpResponseMessage(HttpStatusCode.Accepted);
                //FormsAuthentication.SetAuthCookie(Thread.CurrentPrincipal.Identity.Name,false);
                //actionContext.RequestContext.Principal = Thread.CurrentPrincipal;
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

        }
    }
}