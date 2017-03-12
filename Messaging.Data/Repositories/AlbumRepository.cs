using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Messaging.Data.Interfaces;
using Messaging.Model;
using Newtonsoft.Json;

namespace Messaging.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly string _baseUrl;

        public AlbumRepository(string baseUrl)
        {
            _baseUrl = string.Concat(baseUrl, "/albums");
        }

        public async Task<ICollection<Album>> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(_baseUrl);

                return JsonConvert.DeserializeObject<ICollection<Album>>(response);
            }
        }

        public async Task<ICollection<Album>> GetAllAsync(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{_baseUrl}?userId={userId}");

                return JsonConvert.DeserializeObject<ICollection<Album>>(response);
            }
        }

        public async Task<Album> GetAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{_baseUrl}/{id}");

                return JsonConvert.DeserializeObject<Album>(response);
            }
        }
    }
}
