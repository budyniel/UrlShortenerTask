using Microsoft.EntityFrameworkCore;
using UrlShortener.Data.Data;
using UrlShortener.Domain.Entities.Url;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Data.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly ApiContext _dbContext;

        public ShortUrlRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ShortUrl> GetById(int id)
        {
            return await _dbContext.ShortUrls.FirstOrDefaultAsync(url => url.Id == id);
        }

        public async Task<ShortUrl> GetByToken(string token)
        {
            return await _dbContext.ShortUrls.FirstOrDefaultAsync(url => url.Token == token);
        }

        public async Task<int> Save(ShortUrl url)
        {
            url.Created = DateTime.Now;

            await _dbContext.ShortUrls.AddAsync(url);
            await _dbContext.SaveChangesAsync();

            return url.Id;
        }

        public async Task<IList<ShortUrl>> GetAll()
        {
            return await _dbContext.ShortUrls.ToListAsync();
        }
    }
}
