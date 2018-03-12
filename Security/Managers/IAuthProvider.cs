using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TopTal.JoggingApp.Security.Principals;

namespace TopTal.JoggingApp.Security.Managers
{
    public interface IAuthProvider
    {
        string UserName { get; }
        AppUser CurrentUser { get; }
        bool IsAuthenticated { get; }

        void Authenticate();
        bool Authorized(params Permission[] permissions);
        bool Authorized(Permission[] permissions, bool all);
        void Demand(params Permission[] permissions);
        void Demand(Permission[] permissions, bool demandAll);
    }
}