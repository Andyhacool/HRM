using HRM.Domain.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRM.Application.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The First Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Date format is invalid")]
        [DisplayName("Birth Date")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "The Gender is Required")]
        public Gender Gender { get; set; }

        public string Detail { get; set; }
    }
}
