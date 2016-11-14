using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using TheBooks.Api.ViewModels;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;

namespace TheBooks.Api.Controllers
{
    [Produces("application/json"), Route("book")]
    public class BookController : Controller
    {
        private DbConnection db;

        public BookController(DbConnection db)
        {
            this.db = db;
        }
        
        [HttpPost]
        [ProducesResponse(200, typeof(NewBookResponse))]
        [ProducesResponse(404)]
        public async Task<IActionResult> New([FromQuery] string accessToken, [FromBody] NewBookRequest request)
        {
            var libraryId = await this.db.ExecuteScalarAsync<int>("SELECT id FROM libraries WHERE access_token = @accessToken", new { accessToken });
            if (libraryId == 0)
                return NotFound();

            var param = new { 
                name = request.Name, 
                isbn = request.ISBN,
                pages = request.Pages,
                libraryId = libraryId
            };
            var id = await this.db.ExecuteScalarAsync<int>(@"INSERT INTO books (name, isbn, pages, library_id) values (@name, @isbn, @pages, @libraryId) returning id", param);
            return Ok(new NewBookResponse(id));
        }
        
        [HttpGet, Route("list")]
        [ProducesResponse(200, typeof(IEnumerable<BookListItem>))]
        [ProducesResponse(404)]
        public async Task<IActionResult> List([FromQuery] string accessToken)
        {
            var libraryId = await this.db.ExecuteScalarAsync<int>("SELECT id FROM libraries WHERE access_token = @accessToken", new { accessToken });
            if (libraryId == 0)
                return NotFound();

            var list = await this.db.QueryAsync<BookListItem>("SELECT id, name, isbn, pages FROM books WHERE library_id = @libraryId", new { libraryId });
            return Ok(list);
        }
  }
}