using System.Collections.Generic;

namespace TheBooks.Api.Models
{ 
  public class Library
  {
      public int Id { get; set; }
      public string ApiKey { get; set; }
      public List<Book> Books { get; set; }
  }
}