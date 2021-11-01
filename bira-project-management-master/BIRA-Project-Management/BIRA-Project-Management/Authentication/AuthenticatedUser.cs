using System.Security.Principal;

namespace BIRA_Project_Management.Authentication {
    public class AuthenticatedUser : IIdentity {
        public string AuthenticationType { get; } 
        public bool IsAuthenticated { get; }
        public string Name { get; }

        public AuthenticatedUser(string authenticationType, bool isAuthenticated, string name) {
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            Name = name;
        }
    }
}
