﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LSP.Core.Security
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
