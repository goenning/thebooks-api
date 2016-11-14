using Xunit;
using TheBooks.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using FluentAssertions.Mvc;
using TheBooks.Api.ViewModels;

namespace TheBooks.Tests
{
    [Collection("ControllerTest")]
    public class LibraryControllerTest : ControllerTest<LibraryController>
    {
        public LibraryControllerTest() : base()
        {
            this.controller = new LibraryController(this.connection);
        }

        [Fact]
        public async void ShouldCreateNewLibrary() 
        {
            var newLibrary = await this.ExecuteAction<NewLibraryResponse>(x => x.New(new NewLibraryRequest("The .NET Library")));
            newLibrary.Id.Should().BeGreaterThan(0);
            newLibrary.Name.Should().Be("The .NET Library");
            newLibrary.AccessToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void ShouldBeAbleToGetLibraryByAccessToken_AndBookCountShouldBeZero() 
        {
            var newLibrary = await this.ExecuteAction<NewLibraryResponse>(x => x.New(new NewLibraryRequest("The .NET Library")));
            var library = await this.ExecuteAction<GetLibraryResponse>(x => x.Get(newLibrary.AccessToken));
            library.Id.Should().Be(newLibrary.Id);
            library.Name.Should().Be(newLibrary.Name);
            library.AccessToken.Should().Be(newLibrary.AccessToken);
            library.BookCount.Should().Be(0);
        }

        [Fact]
        public async void ShouldReturn404_WhenGetWithInvalidToken() 
        {
            var result = await this.controller.Get("some-token");
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void ShouldReturn404_WhenDeleteWithInvalidToken() 
        {
            var result = await this.controller.Delete("some-token");
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void ShouldBeAbleToDeleteLibraryByAccessToken() 
        {
            var newLibrary = await this.ExecuteAction<NewLibraryResponse>(x => x.New(new NewLibraryRequest("The .NET Library")));
            await this.ExecuteAction(x => x.Delete(newLibrary.AccessToken));
        }
    }
}
