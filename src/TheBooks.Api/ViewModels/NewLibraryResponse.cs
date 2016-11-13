namespace TheBooks.Api.ViewModels
{ 
  public class NewLibraryResponse
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public string AccessToken { get; set; }

      public NewLibraryResponse()
      {
        
      }

      public NewLibraryResponse(int id, string name, string accessToken)
      {
          this.Id = id;
          this.Name = name;
          this.AccessToken = accessToken;
      }
  }
}