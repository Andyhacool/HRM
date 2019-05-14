using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Responses;
using HRM.Infra.CrossCutting.Identity.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Requests
{
    public class RegisterUserRequest: AuthRequest
    {
        public RegisterUserRequest(string firstName, string lastName, string email, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterUserRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
