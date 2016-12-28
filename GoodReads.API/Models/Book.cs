namespace GoodReads.API.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public Author Author { get; set; }
    }
}
