using System;

namespace TheBooks.Api.ViewModels
{ 
  public class GetLibraryResponse
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public string AccessToken { get; set; }
      public DateTime CreatedOn { get; set; }
      public DateTime ModifiedOn { get; set; }
      public int BookCount { get; set; }
  }
}