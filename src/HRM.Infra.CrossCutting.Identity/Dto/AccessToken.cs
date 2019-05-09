using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Dto
{
    public sealed class AccessToken
    {
        public string Token { get; }
        public int ExpiresIn { get; }

        public AccessToken(string token, int expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }
    }
}
