namespace TheBooks.Api.ViewModels
{ 
  public class NewBookResponse
  {
      public int Id { get; set; }

      public NewBookResponse()
      {
        
      }

      public NewBookResponse(int id)
      {
          this.Id = id;
      }
  }
}