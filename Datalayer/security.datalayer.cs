using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Security
{
    public class DataLayer : Leaf.Data.DataLayerBaseMySQL
    {

        public DataLayer(IConfiguration _config)
            : base(new Data.ConnectionDB(_config).getConnectString())
        {

        }

        /// <summary>
        ///     Checks if user and password exists in DB
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     String with JWT token
        /// </returns>
        public string Login(string user, string password)
        {
            try
            {
                // Prepared statement
                string sql = @"SELECT * 
                               FROM  USERS
                               WHERE USERNAME = @USERNAME AND
                               PASSWORD = @PASSWORD";

                // Gets Datatable from DB
                DataTable dt = GetDataTable(sql, new Parameters("USERNAME", DbType.String, user.ToLower()), new Parameters("PASSWORD", DbType.String, Leaf.Tools.MD5Tools.GetMd5Hash(password)));

                // Checks if there is one exactly one row in DT (If there are more than one, something is wrong) 
                if(dt.Rows.Count == 1)
                {
                    // Generate JWT token with user and profile
                    return Leaf.Tools.JWTTools.GenerateToken(user, GetProfile(user));
                } else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        ///     Gets user profile from DB extracting user form a JWT token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        ///     String with profile's code
        /// </returns>
        public string GetProfileFromToken(string token)
        {
            // Gets username from token
            string user = Leaf.Tools.JWTTools.CheckToken(token);

            // Prepared statement
            string sql = @"SELECT CODE 
                           FROM USERS U, USER_PROFILES P
                           WHERE U.USERNAME = @USERNAME and
                                  U.PROFILEID = P.ID";

            try
            {
                // Gets scalar
                String profile = GetScalar(sql, new Parameters("USERNAME", DbType.String, user)).ToString();
                return profile;
            } catch (Exception ex)
            {
                Debug.WriteLine($"Error getting profile >> {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Gets user profile from DB
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        ///     String with profile's code
        /// </returns>
        public string GetProfile(string user)
        {
            // Prepared statement
            string sql = @"SELECT CODE 
                           FROM USERS U, USER_PROFILES P
                           WHERE U.USERNAME = @USERNAME and
                                  U.PROFILEID = P.ID";

            try
            {
                // Gets scalar
                String profile = GetScalar(sql, new Parameters("USERNAME", DbType.String, user)).ToString();
                return profile;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Errro getting profile >> {ex.Message}");
                return null;
            }
        }
    }
}