using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities.Url;

namespace UrlShortener.Data.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) 
        { }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
