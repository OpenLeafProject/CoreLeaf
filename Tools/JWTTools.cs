using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hnet.Util
{
    public class JWTTools
    {
        private static string Secret = "db3OIsj+BXE9NZDy0t8W56cNekrF+2d/1sFnWG30iTO5tVWJG8a56WvB1GlOgJuQZdcF2Luq" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

        public static string GenerateToken(string user, string app, string permisos, int expireMinutes = 360)
        {
            var symmetricKey = Convert.FromBase64String(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Secret + app)));
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;

                var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>()
                        {
                        new Claim(ClaimTypes.Name, user),
                        new Claim(ClaimTypes.Role, app),
                        new Claim("PERMISOS", permisos),
                    }, "Custom"),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
     
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static string CheckToken(string token, string app)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Secret + app)));

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal.Identities.First().Name;

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

    }
}