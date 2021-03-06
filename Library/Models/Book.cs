﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name ="Author")]
        public int AuthorID { get; set; }
        public string ISBN { get; set; }
        [Display(Name ="Number of Pages")]
        public int NumberOfPages { get; set; }
        public Author Author { get; set; }
    }
}