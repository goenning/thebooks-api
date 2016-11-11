using Xunit;
using TheBooks.Api.Controllers;
using Npgsql;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheBooks.Api.Models;
using FluentAssertions;
using FluentAssertions.Mvc;

namespace TheBooks.Tests
{
    public class BookControllerTest : DbTest
    {
        private BookController controller;
        public BookControllerTest()
        {
            this.controller = new BookController(this.connection);
        }

        [Fact]
        public async void ShouldReturnNewLibraryWhenAccessTokenIsNew() 
        {
            var result = await this.controller.List("a-new-token");
            var ok = result.Should().BeOfType<OkObjectResult>().Subject;
            var books = ok.Value.Should().BeAssignableTo<IEnumerable<Book>>().Subject;
            books.Should().BeEmpty();
        }
    }
}
