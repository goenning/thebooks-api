namespace TheBooks.Api.ViewModels
{ 
  public class NewLibraryRequest
  {
      public string Name { get; set; }

      public NewLibraryRequest()
      {

      }

      public NewLibraryRequest(string name)
      {
          this.Name = name;
      }
  }
}