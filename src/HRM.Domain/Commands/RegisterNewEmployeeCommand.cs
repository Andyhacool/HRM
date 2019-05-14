using HRM.Domain.Models;
using HRM.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Commands
{
    public class RegisterNewEmployeeCommand : EmployeeCommand
    {
        public RegisterNewEmployeeCommand(string firstName, string lastName, Gender gender, string detail, DateTime dob)
        {
            FirstName = firstName;
            LastName = lastName;
            Dob = dob;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewEmployeeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
