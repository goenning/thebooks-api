using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Dapper;
using System.Threading.Tasks;
using System;
using TheBooks.Api.ViewModels;

namespace TheBooks.Api.Controllers
{
    [Produces("application/json"), Route("library")]
    public class LibraryController : Controller
    {
        private DbConnection db;

        public LibraryController(DbConnection db)
        {
            this.db = db;
        }
        
        [HttpPost]
        [ProducesResponse(200, typeof(NewLibraryResponse))]
        [ProducesResponse(404)]
        public async Task<IActionResult> New([FromBody] NewLibraryRequest request)
        {
            var accessToken = Guid.NewGuid().ToString();
            var param = new { name = request.Name, accessToken };
            var id = await this.db.ExecuteScalarAsync<int>(@"INSERT INTO libraries (name, access_token, created_on, modified_on) values (@name, @accessToken, now(), now()) returning id", param);
            return Ok(new NewLibraryResponse(id, request.Name, accessToken));
        }
        
        [HttpGet]
        [ProducesResponse(200, typeof(GetLibraryResponse))]
        [ProducesResponse(404)]
        public async Task<IActionResult> Get([FromQuery] string accessToken)
        {
            var response = await this.db.QueryFirstOrDefaultAsync<GetLibraryResponse>("SELECT id, name, access_token, created_on, modified_on FROM libraries WHERE access_token = @accessToken", new { accessToken });
            if (response == null)
                return NotFound();
            return Ok(response);
        }
        
        [HttpDelete]
        [ProducesResponse(200)]
        [ProducesResponse(404)]
        public async Task<IActionResult> Delete([FromQuery] string accessToken)
        {
            int count = await this.db.ExecuteAsync("DELETE FROM libraries WHERE access_token = @accessToken", new { accessToken });
            if (count == 0)
                return NotFound();
            return Ok();
        }
  }
}