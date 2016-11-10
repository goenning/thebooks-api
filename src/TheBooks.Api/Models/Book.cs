using System.Collections.Generic;

namespace TheBooks.Api.Models
{ 
  public class Book
  {
      public int Id { get; set; }
      public string ISBN { get; set; }
      public string Title { get; set; }
      public int Pages { get; set; }
      public List<Author> Authors { get; set; }
  }
}