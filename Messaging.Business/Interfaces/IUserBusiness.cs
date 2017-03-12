using System.Threading.Tasks;

namespace Messaging.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<string> GetAllUsersAsync(string responseFormat);
        Task<string> GetUserAsync(int id, string responseFormat);
    }
}
