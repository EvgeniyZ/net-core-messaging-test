using System.Threading.Tasks;

namespace Messaging.Business.Interfaces
{
    public interface IAlbumBusiness
    {
        Task<string> GetAllAlbumsAsync(string responseFormat);
        Task<string> GetAlbumAsync(int id, string responseFormat);
        Task<string> GetUserAlbumsAsync(int userId, string responseFormat);
    }
}
