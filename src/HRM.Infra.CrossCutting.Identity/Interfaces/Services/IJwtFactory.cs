using HRM.Infra.CrossCutting.Identity.Dto;
using System.Threading.Tasks;

namespace HRM.Infra.CrossCutting.Identity.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}
