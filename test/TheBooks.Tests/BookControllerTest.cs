using Microsoft.AspNetCore.Mvc;
using TheBooks.Api.Controllers;
using TheBooks.Api.ViewModels;
using Xunit;
using FluentAssertions;
using FluentAssertions.Mvc;

namespace TheBooks.Tests
{
    [Collection("ControllerTest")]
    public class BookControllerTest : ControllerTest<BookController>
    {
        public BookControllerTest() : base()
        {
            this.controller = new BookController(this.connection);
        }

        [Fact]
        public async void ShouldNotCreateBookOnUnknownLibrary() 
        {
            var result = await this.controller.New("any-token", new NewBookRequest("C# for Dummies", 200, "9781118385364"));
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
