using System.Collections.Generic;
using System.Threading.Tasks;
using Messaging.Model;

namespace Messaging.Data.Interfaces
{
    public interface IAlbumRepository
    {
        Task<ICollection<Album>> GetAllAsync();
        Task<ICollection<Album>> GetAllAsync(int userId);
        Task<Album> GetAsync(int id);
    }
}
