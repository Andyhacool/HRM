 
using System.Security.Claims;

namespace HRM.Infra.CrossCutting.Identity.Interfaces.Services
{
    public interface IJwtTokenValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
    }
}
