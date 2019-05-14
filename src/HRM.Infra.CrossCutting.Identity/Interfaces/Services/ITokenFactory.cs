
namespace HRM.Infra.CrossCutting.Identity.Interfaces.Services
{
    public interface ITokenFactory
    {
        string GenerateToken(int size= 32);
    }
}
