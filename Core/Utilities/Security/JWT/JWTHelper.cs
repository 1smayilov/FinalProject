using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } // appsetingsin içini oxuyur _takenoptions a atır
        private TokenOptions _tokenOptions; // appsetings dəki dəyərlər 
        private DateTime _accessTokenExpiration; // acess token nə vaxt keçərsiz olacaq
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); 
            // Configurationdakı (appsetingsdəki) bölümü al və bu sinifin dəyərlərinə at
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); // Token nə vaxt bitəcək, vaxtı configurationdan alır 
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); // Keyi TokenOptionsdan götür, CreateSecurityKey formatında
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); // Hansı keyi və hansı alqoritmi istiafə edəcək 
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); // Bunlardan istifadə edərək token yaradırıq 
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt); 
            // string formata çevirir, tokeni HTTP başlıqlarında və ya URL-lərdə istifadə edilə bilən bir formata çevirir.

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,  // Tokenləri yaratmaq, imzalamaq və doğrulamaq üçün istifadə olunur.
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, // İndiki vaxtdan əvvəl bir vaxt verilə bilməz
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims) 
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
