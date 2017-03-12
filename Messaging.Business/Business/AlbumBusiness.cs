using System;
using System.Linq;
using System.Threading.Tasks;
using Messaging.Business.Helpers;
using Messaging.Business.Interfaces;
using Messaging.Data.Interfaces;
using Messaging.Model;

namespace Messaging.Business.Business
{
    public class AlbumBusiness : IAlbumBusiness
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ISerializerHelper _serializerHelper;

        public AlbumBusiness(IAlbumRepository albumRepository, ISerializerHelper serializerHelper)
        {
            _albumRepository = albumRepository;
            _serializerHelper = serializerHelper;
        }

        public async Task<string> GetAllAlbumsAsync(string responseFormat)
        {
            var albums = await _albumRepository.GetAllAsync();

            return responseFormat.Equals(ResponseFormats.Xml, StringComparison.OrdinalIgnoreCase)
                ? _serializerHelper.XmlSerialize(albums.ToList())
                : _serializerHelper.JsonSerialize(albums);
        }

        public async Task<string> GetAlbumAsync(int id, string responseFormat)
        {
            var album = await _albumRepository.GetAsync(id);

            return responseFormat.Equals(ResponseFormats.Xml, StringComparison.OrdinalIgnoreCase)
                ? _serializerHelper.XmlSerialize(album)
                : _serializerHelper.JsonSerialize(album);
        }

        public async Task<string> GetUserAlbumsAsync(int userId, string responseFormat)
        {
            var albums = await _albumRepository.GetAllAsync(userId);

            return responseFormat.Equals(ResponseFormats.Xml, StringComparison.OrdinalIgnoreCase)
                ? _serializerHelper.XmlSerialize(albums.ToList())
                : _serializerHelper.JsonSerialize(albums);
        }
    }
}
