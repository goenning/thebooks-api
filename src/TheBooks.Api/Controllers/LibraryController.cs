using Microsoft.AspNetCore.Mvc;
using TheBooks.Api.Models;
using System.Data.Common;
using Dapper;
using System.Threading.Tasks;
using System;
using TheBooks.Api.ViewModels;

namespace TheBooks.Api.Controllers
{
    [Produces("application/json"), Route("libraries")]
    public class LibraryController : Controller
    {
        private DbConnection db;

        public LibraryController(DbConnection db)
        {
            this.db = db;
        }
        
        [HttpPost, Route("new")]
        public async Task<IActionResult> CreateNew(string name)
        {
            var accessToken = Guid.NewGuid();
            var param = new { name, accessToken };
            var id = await this.db.ExecuteScalarAsync<int>(@"INSERT INTO libraries (name, access_token, created_on, modified_on) values (@name, @accessToken, now(), now()) returning id", param);
            return Ok(new NewLibrary(id, name, accessToken));
        }
        
        [HttpGet, Route("get")]
        public async Task<IActionResult> Get(string accessToken)
        {
            var header = await this.db.QueryFirstOrDefaultAsync<LibrarySummary>("SELECT id, name, access_token, created_on, modified_on FROM libraries WHERE access_token = @accessToken", new { accessToken });
            if (header == null)
                return NotFound();
            return Ok(header);
        }
        
        [HttpDelete, Route("delete")]
        public async Task<IActionResult> Delete(string accessToken)
        {
            int count = await this.db.ExecuteAsync("DELETE FROM libraries WHERE access_token = @accessToken", new { accessToken });
            if (count == 0)
                return NotFound();
            return Ok();
        }
  }
}