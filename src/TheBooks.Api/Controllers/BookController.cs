using Microsoft.AspNetCore.Mvc;
using TheBooks.Api.Models;
using System.Linq;
using System.Collections.Generic;

namespace TheBooks.Api.Controllers
{
    [Produces("application/json")]
    public class BookController : Controller
    {
        private TheBooksContext db;

        public BookController(TheBooksContext db)
        {
            this.db = db;
        }
        
      [HttpGet, Route("books")]
      public IEnumerable<Book> List(string accessToken)
      {
          var library = this.db.Libraries.FirstOrDefault(x => x.AccessToken == accessToken);
          if (library == null)
          {
              library = new Library() { AccessToken = accessToken };
              this.db.Libraries.Add(library);
              this.db.SaveChanges();
          }
          return library.Books;
      }
  }
}