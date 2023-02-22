using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace UrlShortener.WebApplication.Models
{
    public class UrlModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Url]
        public string OriginalUrl { get; set; }

        [ValidateNever]
        public string ShortUrl { get; set; }

        [ValidateNever]
        public string Token { get; set; }
    }
}
