using UrlShortener.Domain.Base;

namespace UrlShortener.Domain.Entities.Url
{
    public class ShortUrl : BaseEntity
    {
        public string OriginalUrl { get; set; }

        public string Value { get; set; }

        public string Token { get; set; }
    }
}
