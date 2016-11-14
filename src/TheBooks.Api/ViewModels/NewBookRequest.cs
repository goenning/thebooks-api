namespace TheBooks.Api.ViewModels
{ 
    public class NewBookRequest
    {
        public string Name { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }

        public NewBookRequest()
        {

        }

        public NewBookRequest(string name, int pages, string isbn)
        {
            this.Name = name;
            this.Pages = pages;
            this.ISBN = isbn;
        }
    }
}