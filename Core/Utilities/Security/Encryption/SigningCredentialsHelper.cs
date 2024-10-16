using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    // JWT servislərinin, WEB Api nin istifadə edə biləcəyəyi JWB tokenlərinin yaradılması üçündür (Email, Password) 
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) // İmza
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); 
                // Asp.Net açar olaraq bu key i istifadə et, şifrələmə olaraq da Sha 512 ni istifadə et
        }
    }
}
