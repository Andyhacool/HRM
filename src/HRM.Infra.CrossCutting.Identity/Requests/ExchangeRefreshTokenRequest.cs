using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Responses;
using HRM.Infra.CrossCutting.Identity.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Requests
{
    public class ExchangeRefreshTokenRequest : AuthRequest
    {
        public ExchangeRefreshTokenRequest(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public override bool IsValid()
        {
            ValidationResult = new ExchangeRefreshTokenRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
