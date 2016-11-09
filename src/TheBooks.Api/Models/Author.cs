using System.Collections.Generic;

namespace TheBooks.Api.Models
{ 
  public class Author
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public List<BookAuthor> Books { get; set; }
  }
}