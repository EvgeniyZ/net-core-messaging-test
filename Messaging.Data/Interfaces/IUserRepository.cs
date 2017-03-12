using System.Collections.Generic;
using System.Threading.Tasks;
using Messaging.Model;

namespace Messaging.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllAsync();
        Task<User> GetAsync(int id);
    }
}
