using System.Collections.Generic;

namespace TheBooks.Api.Models
{ 
  public class Library
  {
      public int Id { get; set; }
      public string AccessToken { get; set; }
      public List<Book> Books { get; set; }

      public Library()
      {
        this.Books = new List<Book>();
      }
  }
}