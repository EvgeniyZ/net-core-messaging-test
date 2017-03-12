using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Messaging.Data.Interfaces;
using Messaging.Model;
using Newtonsoft.Json;

namespace Messaging.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _baseUrl;

        public UserRepository(string baseUrl)
        {
            _baseUrl = string.Concat(baseUrl, "/users");
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(_baseUrl);

                return JsonConvert.DeserializeObject<ICollection<User>>(response);
            }
        }

        public async Task<User> GetAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{_baseUrl}/{id}");

                return JsonConvert.DeserializeObject<User>(response);
            }
        }
    }
}
