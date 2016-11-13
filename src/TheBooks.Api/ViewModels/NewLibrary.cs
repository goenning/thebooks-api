using System;

namespace TheBooks.Api.ViewModels
{ 
  public class NewLibrary
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public Guid AccessToken { get; set; }

      public NewLibrary()
      {
        
      }

      public NewLibrary(int id, string name, Guid accessToken)
      {
          this.Id = id;
          this.Name = name;
          this.AccessToken = accessToken;
      }
  }
}