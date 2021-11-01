using Microsoft.AspNetCore.Authorization;

namespace BIRA_Project_Management {
    public class BasicAuthorizationAttribute : AuthorizeAttribute {
        public BasicAuthorizationAttribute() {
            Policy = "BasicAuthentication";
        }
    }
}
