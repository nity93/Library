using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Book:BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name ="Author")]
        public int AuthorID { get; set; }
        public string ISBN { get; set; }
        [Display(Name ="Number of Pages")]
        public int NumberOfPages { get; set; }
     
    }
}