using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    // Şifrələmə olan sistemlərdə biz hər şeyi byte[] formatında verməliyik (ASP.Netin Json WEb Token servislərinin başa düşəcəyi hala gətiririk)
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) // apsetings dəki security keyi ver mən də sənə qarşılığını verim
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
