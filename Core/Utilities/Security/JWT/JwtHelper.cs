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

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        //Configuration gördüğümüz yerde appsettingsjson dosyasına gidip okumamızı sağlıyor
        public IConfiguration Configuration { get; } //appsettingsdeki dosyadaki değerleri okumamıza yarıyor
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration) 
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//TokenOptions bölümünü al Get'in içindeki sınıfla eşitle 
            //GetSection - appsettingjson'da oluşturduğumuz herbir token'a denilir , mesela TokenOptions bir Sectiondır
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //Token da olması gerekenler 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//Token ne zaman bitecek - yandaki kod şuandan itibaren dakika ekler - 10 dakika ekledik
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //tokenOption - appsettingsjsonda oluşturduğumuz değerlerdir - bu kodlarda onları okumamızı sağlıyor
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//kullanacağımız algoritma
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//jsonwebtoken olması gerekenler 
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims) 
        {
            //jsonwebtoken oluşturdum istedikleri bilgileride yazıyorum
            var jwt = new JwtSecurityToken(
                //Tüm bilgileri oluşturduk.
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //Şimdiki tarihten önceki tarih veremeyiz
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }
        //Claim listesi oluşturduk 
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");// iki stringi yan yana göstermek için böyle yazdık
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
