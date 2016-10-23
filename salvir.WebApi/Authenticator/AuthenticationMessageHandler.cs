using depross.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using depross.ViewModel;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace salvir.WebApi.Authenticator
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
                var username = parts[0];
                var password = parts[1];

                if (!AuthenticateUser(username, password))
                {
                    // Authentication failed                     
                    response =
    request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                    response.Headers.Add("WWW-Authenticate", "Basic");

                    return response;
                }
            }

            response = await base.SendAsync(request, cancellationToken);

            // Add the required authentication type to the response 
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                response.Headers.Add("WWW-Authenticate", "Basic");
            }

            return response;
        }

        private bool AuthenticateUser(string username, string password)
        {
            AccountWebService _accountService = new AccountWebService();

            // Use a simplified authentication check where username must be equal to password 
            AccountLoginDTO account = new AccountLoginDTO { UserName = username, Password = password };
            var user = _accountService.Login(account);
            if (user != null)
            {
                // User is valid. Create a principal for the user 
                IIdentity identity = new GenericIdentity(user.Email);
                return true;
            }
            return false;
        }
    }
}