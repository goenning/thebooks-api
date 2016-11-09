using Microsoft.AspNetCore.Mvc;

namespace TheBooks.Api.Controllers
{ 
  public class BookController : Controller
  {
      [Route("books")]
      public IActionResult Index()
      {
          return Ok("Hello World from a controller");
      }
  }
}