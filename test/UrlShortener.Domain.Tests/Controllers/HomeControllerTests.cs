using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities.Url;
using UrlShortener.WebApplication.Controllers;
using UrlShortener.WebApplication.Extentions;
using UrlShortener.WebApplication.Models;
using UrlShortener.WebApplication.Services.Url;
using Xunit;

namespace UrlShortener.Domain.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IUrlService> _mockService;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _mockService = new Mock<IUrlService>();
            _controller = new HomeController(_mockService.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnsExactNumberOfLinks()
        {
            //Arrange
            _mockService.Setup(service => service.GetAllLinks())
                .Returns(Task.FromResult(new List<ShortUrl>{ new(), new() }));

            //Act
            var result = await _controller.ShowAllLinks();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var links = Assert.IsType<List<UrlModel>>(viewResult.Model);
            Assert.Equal(2, links.Count);
        }

        [Fact]
        public async void Index_ActionExecutes_RedirectsOnValidInput()
        {
            //Arrange
            var model = new UrlModel
            {
                OriginalUrl = "www.linkedin.com/in/danszewski"
            };

            //Act
            var result = await _controller.ShortenUrl(model);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnsIndexOnInvalidInput()
        {
            //Arrange
            var model = new UrlModel
            {
                OriginalUrl = "invalid input"
            };
            _controller.ModelState.AddModelError("key", "error message");

            //Act
            var result = await _controller.ShortenUrl(model);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsShortenedLink()
        {
            //Arrange
            var model = new UrlModel
            {
                OriginalUrl = "www.linkedin.com/in/danszewski"
            };

            //Act
            var result = _controller.CreateShortLink(model);

            //Assert
            Assert.Equal(result.Value, $"https://short.url/{result.Token}");
        }

        [Fact]
        public void Index_ActionExecutes_LinkIsCorrectlyEncoded()
        {
            //Arrange
            var model = new UrlModel
            {
                OriginalUrl = "www.linkedin.com/in/danszewski"
            };

            //Act
            var result = _controller.CreateShortLink(model);

            //Assert
            var token = UrlEncoder.Encode(result.Id);
            var id = UrlEncoder.Decode(result.Token);
            Assert.Equal(result.Id, id);
            Assert.Equal(result.Token, token);
        }
    }
}
