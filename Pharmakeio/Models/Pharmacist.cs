using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmakeio.Models
{
    public class Pharmacist
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "You have to provide a vaild full name")]
        [MaxLength(50, ErrorMessage ="The Name musn't exceed 50 char")]
        [MinLength(5, ErrorMessage ="The Name musn't be less than 5 char")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "you should enter the Specialization")]
        public string Specialization { get; set; }

        [Range(6000, 35000, ErrorMessage ="the Salary must be between (6k,35k)")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid Phone Number")]
        public string  PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string Code { get; set; }

        [DataType(DataType.Password)]
        [Compare("Code", ErrorMessage ="Code and Confirm Code should be same")]
        [NotMapped]
        public string ConfirmCode{ get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string? ImagePath { get; set; }



    }
}
