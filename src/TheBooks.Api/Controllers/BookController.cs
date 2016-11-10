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
            var library = this.db.QueryFirstOrDefault<Library>("SELECT \"Id\", \"AccessToken\" FROM \"Libraries\" WHERE \"AccessToken\" = @accessToken", new { accessToken });
            if (library == null)
                library = new Library() { AccessToken = accessToken };
            return library.Books;
        }
  }
}