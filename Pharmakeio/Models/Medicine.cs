using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pharmakeio.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        [DisplayName("Medicine Name")]
        [Required(ErrorMessage = "You have to provide a vaild Medicine Name")]
        [MaxLength(50, ErrorMessage = "The Medicine Name musn't exceed 50 char")]
        [MinLength(5, ErrorMessage = "The Medicine Name musn't be less than 5 char")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "You have to provide a vaild Description")]
        [MaxLength(100, ErrorMessage = "The Description musn't exceed 50 char")]
        [MinLength(10, ErrorMessage = "The Description musn't be less than 5 char")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [ValidateNever]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [DisplayName("Shelf Life per month")]
        [Required(ErrorMessage = "you should enter the Shelf Life for the Medicine")]
        public int ShelfLife { get; set; }

        [Required(ErrorMessage = "you should enter the Price of Medicine")]
        public decimal Price { get; set; }

        [ValidateNever]
        public DateTime LastUpdate { get; set; }

        [ValidateNever]
        public DateTime CreatedAt { get; set;}

       [DisplayName("Pharmaceutical Department")]
        [Range(1, int.MaxValue, ErrorMessage = "choose a valid number")]
        public int PharmaceuticalDepartmentId { get; set; }

        [ValidateNever]// navigation property 
        public PharmaceuticalDepartment PharmaceuticalDepartment { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string? ImagePath { get; set; }




    }
}
