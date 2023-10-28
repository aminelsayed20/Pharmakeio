using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmakeio.Models
{
    public class PharmaceuticalDepartment
    {
        public int Id{ get; set; }

        [DisplayName("medicines Department Name")]
        [Required(ErrorMessage = "You have to provide a vaild Pharmaceutical Department")]
        [MaxLength(50, ErrorMessage = "The Name musn't exceed 50 char")]
        [MinLength(5, ErrorMessage = "The Name musn't be less than 5 char")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "You have to provide a vaild Description")]
        [MaxLength(100, ErrorMessage = "The Description musn't exceed 50 char")]
        [MinLength(10, ErrorMessage = "The Description musn't be less than 5 char")]
        public string Description { get; set; }

        [DataType(DataType.Password)]
        public string Code { get; set; }

        [DataType(DataType.Password)]
        [Compare("Code", ErrorMessage = "Code and Confirm Code should be same")]
        [NotMapped]
        public string ConfirmCode { get; set; }

        [ValidateNever]
        public DateTime CreatedAt { get; set; }

        [ValidateNever]
        public int NumberOfProducts { get; set; }

       [ValidateNever]
        public List<Medicine> Medicines { get; set; }

    }
}
