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

namespace salvir.WebApi.Authenticator
{
    public class AuthenticationMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage
            request, CancellationToken cancellationToken)
        {
            try
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
                    var username = parts[0];
                    var id = parts[1];
                    AuthenticateUser(username);
                }

                response = await base.SendAsync(request, cancellationToken);

                // Add the required authentication type to the response 
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    response.Headers.Add("WWW-Authenticate", "Basic");
                }

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void AuthenticateUser(string username, string password)
        {
            AccountWebService _accountService = new AccountWebService();
            if (!string.IsNullOrWhiteSpace(username)) { 
            if (_accountService.IsEmailInDatabase(username))
            {
                IIdentity identity = new GenericIdentity(username);
                IPrincipal principal = new GenericPrincipal(identity, new string[] {});
                Thread.CurrentPrincipal = principal;
            }
            }
        }
        private void AuthenticateUser(string username)
        {
            var cookieuser =FormsAuthentication.GetAuthCookie(username, false);
            
            AccountWebService _accountService = new AccountWebService();
            if (!string.IsNullOrWhiteSpace(username))
            {
                IPrincipal principal;
                var account = _accountService.GetAccount(username);
                if (account != null)
                {
                    IIdentity identity = new GenericIdentity(username);
                    if (account.Company != null)
                    {
                        principal = new GenericPrincipal(identity, new string[] { "Seller" });
                    }
                    else
                    {
                        principal = new GenericPrincipal(identity, new string[] { "Basic" });
                    }
                    Thread.CurrentPrincipal = principal;
                }
            }
        }

    }
}