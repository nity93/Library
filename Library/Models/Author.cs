using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Author : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name="First Name")]
        [DataType(DataType.Text, ErrorMessage = "First name must be a string.")]
        [StringLength(100, ErrorMessage = "First name can't exceed {0} characters.")]
        
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Author")]
        public string FullName {
         get {
                return FirstName + " " + LastName;
             }
        }




    }
}