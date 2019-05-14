using FluentValidation;
using HRM.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Validations
{
    public abstract class EmployeeValidation<T> : AbstractValidator<T> where T : EmployeeCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Please ensure you have entered the First Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.Dob)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("The employee must have 18 years or more");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("A valid email address is required."); ;
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Employee Id is required.");
        }

        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
