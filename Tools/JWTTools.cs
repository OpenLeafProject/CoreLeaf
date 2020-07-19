using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Leaf.Tools
{
    public class JWTTools
    {
        // Init Secret var
        private static string Secret = Environment.GetEnvironmentVariable("JWTSecret") + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

        /// <summary>
        ///     Generates a JWT token with:
        ///      - Symetric key from launchSettings.json
        ///      - username
        ///      - profile
        ///      - granted routes list
        ///      - expire time
        /// </summary>
        /// <param name="user"> Username </param>
        /// <param name="grantedRoutes"> Routes with access </param>
        /// <param name="expireMinutes"> Alive time for the token </param>
        /// <returns></returns>
        public static string GenerateToken(string user, string profile, int expireMinutes = 360)
        {
            // Get Byte Array from the secret
            Byte[] symmetricKey = Convert.FromBase64String(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Secret)));

            // Creates token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Get now Datetime
            DateTime now = DateTime.UtcNow;

            // Defines SecurityTokenDescriptor
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>() {
                        // Adds username as Name
                        new Claim(ClaimTypes.Name, user),
                        // Adds profile code as Role
                        new Claim(ClaimTypes.Role, profile),
                        /* Code for Custom param
                            new Claim("GRANTEDROUTES", grantedRoutes),
                        */
                    }, "LeafJWT"),

                // Define expiration time
                Expires = now.AddMinutes(expireMinutes),
                
                // Adds keys to token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate token and converts into a serialized string
            SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(stoken);

            // Returns token
            return token;
        }

        /// <summary>
        ///     Checks if token is valid 
        /// </summary>
        /// <param name="token"> JWT token </param>
        /// <returns> Returs string with username if is valid or null for invalid token </returns>
        public static string CheckToken(string token)
        {
            try
            {
                // Creates token handler and reads the token
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // if token es null, returns null
                if (jwtToken == null)
                {
                    return null;
                }

                // Get Byte Array from the secret
                Byte[] symmetricKey = Convert.FromBase64String(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Secret)));

                // Create validation parameters
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                /* Checks if token is valid
                 * - If token is invalid, throw an error
                 * - If token is valid returs the usesname found in token
                 */
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                // Returns token username
                return principal.Identities.First().Name;
            }

            catch (Exception ex)
            {
                // When get an error, return null
                Debug.WriteLine($"[Error validating token]] >> {ex.Message}");
                return null;
            }
        }

    }
}