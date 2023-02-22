using UrlShortener.Domain.Entities.Url;

namespace UrlShortener.Domain.Interfaces
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl> GetById(int id);

        Task<ShortUrl> GetByToken(string token);

        Task<int> Save(ShortUrl Value);

        Task<IList<ShortUrl>> GetAll();
    }
}
