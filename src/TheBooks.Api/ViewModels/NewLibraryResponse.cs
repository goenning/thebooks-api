using System;

namespace TheBooks.Api.ViewModels
{ 
  public class NewLibraryResponse
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public Guid AccessToken { get; set; }

      public NewLibraryResponse()
      {
        
      }

      public NewLibraryResponse(int id, string name, Guid accessToken)
      {
          this.Id = id;
          this.Name = name;
          this.AccessToken = accessToken;
      }
  }
}