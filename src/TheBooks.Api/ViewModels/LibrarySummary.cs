using System;

namespace TheBooks.Api.ViewModels
{ 
  public class LibrarySummary
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public Guid AccessToken { get; set; }
      public DateTime CreatedOn { get; set; }
      public DateTime ModifiedOn { get; set; }
      public int BookCount { get; set; }
  }
}