using deprosa.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using deprosa.ViewModel;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace deprosa.WebApi.Authenticator
{
    public class AuthenticationMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage
            request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;

            if (request.Headers.Authorization != null &&
                request.Headers.Authorization.Scheme == "Basic")
            {
                // Extract the username and password from the HTTP Authorization header 
                var encodedUserPass = request.Headers.Authorization.Parameter.Trim();
                var userPass =
    Encoding.Default.GetString(Convert.FromBase64String(encodedUserPass));
                var parts = userPass.Split(":".ToCharArray());
                var id = parts[0];
                var username = parts[1];
                AuthenticateUser(id);
            }

            response = await base.SendAsync(request, cancellationToken);

            // Add the required authentication type to the response 
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                response.Headers.Add("WWW-Authenticate", "Basic");
            }

            return response;
        }

        private void AuthenticateUser(string id)
        {
            AccountWebService accountService = new AccountWebService();
            if (!string.IsNullOrWhiteSpace(id))
            {
                int currentid;
                if (int.TryParse(id, out currentid) && currentid>0)
                {
                    var account = accountService.GetAccount(currentid);
                    if (account != null)
                    {
                        IIdentity identity = new GenericIdentity(id);
                        IPrincipal principal;
                        if (account.CompanyID > 0)
                        {
                            principal = new GenericPrincipal(identity, new string[] {"Seller"});
                        }
                        else
                        {
                            principal = new GenericPrincipal(identity, new string[] {"Basic"});
                        }
                        Thread.CurrentPrincipal = principal;
                    }
                }
            }
        }

    }
}