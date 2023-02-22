using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain.Entities.Url;
using UrlShortener.WebApplication.Extentions;
using UrlShortener.WebApplication.Models;
using UrlShortener.WebApplication.Services.Url;

namespace UrlShortener.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlService _urlService;

        public HomeController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllLinks()
        {
            var links = await _urlService.GetAllLinks();
            if (links == null)
            {
                return NotFound();
            }

            var model = links.Select(link => new UrlModel
            {
                Id = link.Id,
                OriginalUrl = link.OriginalUrl,
                ShortUrl = link.Value,
                Token = link.Token
            }).ToList();

            return View("ShowAllLinks", model);
        }

        [HttpPost]
        public async Task<ActionResult> ShortenUrl(UrlModel model)
        {
            if (ModelState.IsValid)
            {
                var shortUrl = CreateShortLink(model);
                await _urlService.Save(shortUrl);

                return RedirectToAction(actionName: nameof(Show), routeValues: new { id = shortUrl.Id });
            }

            return View("Index", model);
        }

        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var shortUrl = await _urlService.GetById(id.Value);
            if (shortUrl == null)
            {
                return NotFound();
            }

            ViewData["Path"] = shortUrl.Token;

            var model = new UrlModel
            {
                OriginalUrl = shortUrl.OriginalUrl,
                ShortUrl = shortUrl.Value,
                Token = shortUrl.Token
            };

            return View("Index", model);
        }

        [HttpGet("/Home/RedirectTo/{path:required}", Name = "RedirectTo")]
        public async Task<IActionResult> RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            var shortUrl = await _urlService.GetByToken(path);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Redirect(shortUrl.OriginalUrl);
        }

        public ShortUrl CreateShortLink(UrlModel model)
        {
            var rand = new Random();
            var id = rand.Next();
            var token = UrlEncoder.Encode(id);

            return new ShortUrl
            {
                Id = id,
                OriginalUrl = $"{model.OriginalUrl}",
                Value = $"https://short.url/{token}",
                Token = token,
            };
        }
    }
}