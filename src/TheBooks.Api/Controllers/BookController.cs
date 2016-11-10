using Microsoft.AspNetCore.Mvc;
using TheBooks.Api.Models;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;

namespace TheBooks.Api.Controllers
{
    [Produces("application/json")]
    public class BookController : Controller
    {
        private DbConnection db;

        public BookController(DbConnection db)
        {
            this.db = db;
        }
        
        [HttpGet, Route("books")]
        public IEnumerable<Book> List(string accessToken)
        {
            var library = this.db.QueryFirstOrDefault<Library>("SELECT id, access_token FROM libraries WHERE access_token = @accessToken", new { accessToken });
            if (library == null) {
                this.db.Execute(@"INSERT INTO libraries (access_token, created_on, modified_on) values (@accessToken, now(), now())",  new { accessToken });
                library = new Library() { AccessToken = accessToken };
            }
            return library.Books;
        }
  }
}