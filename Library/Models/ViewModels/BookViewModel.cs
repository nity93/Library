using GoodReads.API.Models;
using System.Collections.Generic;

namespace Library.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public List<Author> Authors { get; set; }

        public List<Work> Works { get; set; }

        public List<Book> Books { get; set; }
        public Author Author { get; set; }
    }
}