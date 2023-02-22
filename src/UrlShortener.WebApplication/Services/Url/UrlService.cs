using UrlShortener.Domain.Entities.Url;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.WebApplication.Services.Url
{
    public class UrlService : IUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public UrlService(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ShortUrl> GetById(int id)
        {
            return await _shortUrlRepository.GetById(id);
        }

        public async Task<int> Save(ShortUrl url)
        {
            return await _shortUrlRepository.Save(url);
        }

        public async Task<ShortUrl> GetByToken(string token)
        {
            return await _shortUrlRepository.GetByToken(token);
        }

        public async Task<List<ShortUrl>> GetAllLinks()
        {
            var links = await _shortUrlRepository.GetAll();
            return links.ToList();
        }
    }
}
