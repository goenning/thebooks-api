using Microsoft.AspNetCore.Mvc;

namespace TheBooks.Api
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