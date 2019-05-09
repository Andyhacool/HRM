using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Employee = "Employee", Id = "id";
            }

            public static class JwtClaims
            {
                public const string Read = "Read", Create = "Create";
            }
        }
    }
}
