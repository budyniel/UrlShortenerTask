using UrlShortener.Domain.Entities.Url;

namespace UrlShortener.WebApplication.Services.Url
{
    public interface IUrlService
    {
        Task<ShortUrl> GetById(int id);

        Task<int> Save(ShortUrl url);

        Task<ShortUrl> GetByToken(string token);

        Task<List<ShortUrl>> GetAllLinks();
    }
}
