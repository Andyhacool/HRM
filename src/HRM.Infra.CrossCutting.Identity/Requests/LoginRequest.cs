using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Responses;
using HRM.Infra.CrossCutting.Identity.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Requests
{
    public class LoginRequest : AuthRequest
    {
        public LoginRequest(string userName, string password, string remoteIpAddress)
        {
            UserName = userName;
            Password = password;
            RemoteIpAddress = remoteIpAddress;
        }

        public override bool IsValid()
        {
            ValidationResult = new LoginRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
