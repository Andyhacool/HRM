using FluentValidation;
using HRM.Infra.CrossCutting.Identity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Validations
{
    public class AuthValidation<T> : AbstractValidator<T> where T : AuthRequest
    {
        protected void ValidateExchangeRefreshToken()
        {
            RuleFor(x => x.AccessToken).NotEmpty().WithMessage("An access token is required");
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("A refresh token is required");
        }

        protected void ValidateUserNamePassword()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(3, 255);
            RuleFor(x => x.Password).NotEmpty().Length(6, 15);
        }

        protected void ValidateFirstAndLastName()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(2, 30);
            RuleFor(x => x.LastName).NotEmpty().Length(2, 30);     
        }
        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("A valid email address is required."); ;
        }
    }
}
