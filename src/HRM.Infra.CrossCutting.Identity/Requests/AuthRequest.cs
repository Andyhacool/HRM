using HRM.Domain.Core.Commands;
using HRM.Domain.Core.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Requests
{
    public abstract class AuthRequest : Command
    {
        public string UserName { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public string RemoteIpAddress { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string AccessToken { get; protected set; }
        public string RefreshToken { get; protected set; }
        public string SigningKey { get; protected set; }
    }
}
