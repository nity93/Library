using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public List<Author> Authors { get; set; }


    }
}