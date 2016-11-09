using System.Collections.Generic;

namespace TheBooks.Api.Models
{ 
  public class Book
  {
      public int Id { get; private set; }
      public string ISBN { get; private set; }
      public string Title { get; private set; }
      public int Pages { get; private set; }
      public IEnumerable<Author> Authors { get; private set; }
  }
}