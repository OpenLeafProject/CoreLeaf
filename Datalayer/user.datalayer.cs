using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.User
{
    public class DataLayer : Leaf.Data.DataLayerBaseMySQL
    {

        public DataLayer(IConfiguration _config)
            : base(new Data.ConnectionDB(_config).getConnectString())
        {

        }

        internal DataTable GetById(int id)
        {
            try
            {
                string sql = @"SELECT * FROM users WHERE id = @ID";

                return GetDataTable(sql, new Parameters("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal DataTable GetByUsername(string username)
        {
            try
            {
                string sql = @"SELECT * FROM users WHERE username = @USERNAME";

                return GetDataTable(sql, new Parameters("USERNAME", DbType.String, username));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.User user)
        {

                string sql = @"INSERT INTO `users` 
                                (`name`, `surname`, `lastname`, 
                                `username`, `password`, `colnum`, `email`, 
                                `creationdate`, `active`, `profileimage`, `lastaccess`, 
                                `lasipaccess`, `profileid`) 
                               VALUES
	                           (@NAME, @SURNAME, @LASTNAME, @USERNAME, @PASSWORD, @COLNUM, @EMAIL, NOW(), @ACTIVE, 
                                @PROFILEIMAGE, NULL, NULL, @PROFILEID)";

                return Execute(sql, new Parameters("NAME", DbType.String, user.Name)
                                       , new Parameters("SURNAME", DbType.String, user.Surname)
                                       , new Parameters("LASTNAME", DbType.String, user.Lastname)
                                       , new Parameters("USERNAME", DbType.String, user.Username)
                                       , new Parameters("PASSWORD", DbType.String, user.Password)
                                       , new Parameters("COLNUM", DbType.String, user.Colnum)
                                       , new Parameters("EMAIL", DbType.String, user.Email)
                                       , new Parameters("ACTIVE", DbType.Int32, user.Active)
                                       , new Parameters("PROFILEIMAGE", DbType.String, user.ProfileImage)
                                       , new Parameters("PROFILEID", DbType.Int32, user.UserProfile.Id)
                                       );

        }

        internal int Save(Models.User user)
        {
                string sql = @"UPDATE `patients` 
                               SET 
                                `name` = @NAME, 
                                `surname` = @SURNAME, 
                                `lastname` = @LASTNAME, 
                                `username` = @USERNAME, 
                                `password` = @PASSWORD, 
                                `colnum` = @COLNUM, 
                                `email` = @EMAIL, 
                                `active` = @ACTIVE, 
                                `profileimage` = @PROFILEIMAGE, 
                                `profileid` = @PROFILEID, 
                               WHERE `id` = @Id";

                return Execute(sql, new Parameters("ID", DbType.Int32, user.Id)
                                       , new Parameters("NAME", DbType.String, user.Name)
                                       , new Parameters("SURNAME", DbType.String, user.Surname)
                                       , new Parameters("LASTNAME", DbType.String, user.Lastname)
                                       , new Parameters("USERNAME", DbType.String, user.Username)
                                       , new Parameters("PASSWORD", DbType.String, user.Password)
                                       , new Parameters("COLNUM", DbType.String, user.Colnum)
                                       , new Parameters("EMAIL", DbType.String, user.Email)
                                       , new Parameters("ACTIVE", DbType.Int32, user.Active)
                                       , new Parameters("PROFILEIMAGE", DbType.String, user.ProfileImage)
                                       , new Parameters("PROFILEID", DbType.Int32, user.UserProfile.Id)
                                       );
        }

        internal int UpdateLastAccess(Models.User user)
        {
            string sql = @"UPDATE `patients` 
                               SET 
                                `lastaccess` = NOW(), 
                                `lastipadress` = @LASTIPADRESS
                               WHERE `id` = @Id";

            return Execute(sql, new Parameters("ID", DbType.Int32, user.Id)
                                   , new Parameters("LASTIPADRESS", DbType.String, user.LastIPAdress)
                                   );
        }
    }
}