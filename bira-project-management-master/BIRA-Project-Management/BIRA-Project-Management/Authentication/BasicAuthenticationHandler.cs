using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIRA_Project_Management.Authentication {
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
        public static string user;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options,
                logger, encoder, clock) {}

        protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
            Response.Headers.Add("WWW-Authenticate", "Basic");
            if (!Request.Headers.ContainsKey("Authorization")) {
                return Task.FromResult(AuthenticateResult.Fail(BiraEnums
                    .message.Authorization_Header_Missing.ToString()));
            }
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var authHeaderRegex = new Regex(@"Basic (.*)");
            if (!authHeaderRegex.IsMatch(authorizationHeader)) {
                return Task.FromResult(AuthenticateResult.Fail(BiraEnums.message
                    .Authorization_Code_Not_Formatted_Properly.ToString()));
            }
            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.
                Replace(authorizationHeader, "$1")));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
            var authUsername = authSplit[0];
            user = authUsername;
            var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception(BiraEnums
                .message.Unable_To_Get_Password.ToString());
            if (authUsername != "root" || authPassword != "root") {
                return Task.FromResult(AuthenticateResult.Fail(BiraEnums
                    .message.The_Username_Or_Password_Is_Not_Correct.ToString()));
            }
            var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, "root");
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));
            return Task.FromResult(AuthenticateResult
                .Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}
