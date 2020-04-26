using System.Security.Claims;
using System.Security.Principal;

namespace TreinoAPI.Claims
{
    public static class IdentityExtensions
    {
        public static int GetIDUsuario(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("IDUsuario");

            if (claim == null)
                return 0;

            return int.Parse(claim.Value);
        }
    }
}
