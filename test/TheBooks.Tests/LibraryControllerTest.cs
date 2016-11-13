using Xunit;
using TheBooks.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using FluentAssertions.Mvc;
using TheBooks.Api.ViewModels;
using System;
using System.Threading.Tasks;

namespace TheBooks.Tests
{
    public class LibraryControllerTest : DbTest
    {
        private LibraryController controller;
        public LibraryControllerTest()
        {
            this.controller = new LibraryController(this.connection);
        }

        private async Task ExecuteAction(Func<LibraryController, Task<IActionResult>> action)
        {
            var result = await action(this.controller);
            result.Should().BeOfType<OkResult>();
        }

        private async Task<T> ExecuteAction<T>(Func<LibraryController, Task<IActionResult>> action)
        {
            var result = await action(this.controller);
            var ok = result.Should().BeOfType<OkObjectResult>().Subject;
            return ok.Value.Should().BeAssignableTo<T>().Subject;
        }

        [Fact]
        public async void ShouldCreateNewLibrary() 
        {
            var library = await this.ExecuteAction<NewLibrary>(x => x.CreateNew("The .NET Library"));
            library.Id.Should().BeGreaterThan(0);
            library.Name.Should().Be("The .NET Library");
            library.AccessToken.Should().NotBeEmpty();
        }

        [Fact]
        public async void ShouldBeAbleToGetLibraryByAccessToken_AndBookCountShouldBeZero() 
        {
            var newLibrary = await this.ExecuteAction<NewLibrary>(x => x.CreateNew("The .NET Library"));
            var library = await this.ExecuteAction<LibrarySummary>(x => x.Get(newLibrary.AccessToken.ToString()));
            library.Id.Should().Be(newLibrary.Id);
            library.Name.Should().Be(newLibrary.Name);
            library.BookCount.Should().Be(0);
        }

        [Fact]
        public async void ShouldReturn404_WhenGetWithInvalidToken() 
        {
            var result = await this.controller.Get("invalid-token");
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void ShouldReturn404_WhenDeleteWithInvalidToken() 
        {
            var result = await this.controller.Delete("invalid-token");
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void ShouldBeAbleToDeleteLibraryByAccessToken() 
        {
            var newLibrary = await this.ExecuteAction<NewLibrary>(x => x.CreateNew("The .NET Library"));
            await this.ExecuteAction(x => x.Delete(newLibrary.AccessToken.ToString()));
        }
    }
}
