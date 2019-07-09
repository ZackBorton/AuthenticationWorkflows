using System.Threading.Tasks;

namespace Logic
{
    public interface IJsonWebTokenLogic
    {
        Task<object> GenerateJwtToken(string email, string user);
    }
}