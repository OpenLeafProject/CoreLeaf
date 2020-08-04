using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class User
    {
        private int id;
        private string username;
        private string password;
        private string name;
        private string surname;
        private string lastname;
        private string colnum;
        private string email;
        private DateTime creationDate;
        private int active;
        private string profileImage;
        private DateTime lastAccess;
        private string lastIPAdress;
        private UserProfile userProfile;

        private readonly IConfiguration _config;

        public int Id { get => id; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Colnum { get => colnum; set => colnum = value; }
        public string Email { get => email; set => email = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public int Active { get => active; set => active = value; }
        public string ProfileImage { get => profileImage; set => profileImage = value; }
        public DateTime LastAccess { get => lastAccess; set => lastAccess = value; }
        public string LastIPAdress { get => lastIPAdress; set => lastIPAdress = value; }
        public UserProfile UserProfile { get => userProfile; set => userProfile = value; }

        public User(IConfiguration config)
        {
            _config = config;
        }

        public User(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public User(string username, IConfiguration config)
        {
            _config = config;
            LoadByUsername(username);
        }

        public User(Dictionary<string, string> values, IConfiguration config)
        {
            _config = config;

            try
            {
                this.id = Int32.Parse(values["id"]);
                LoadById(this.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                this.id = -1;
            }

            Name            = values["name"];
            Surname         = values["surname"];
            Lastname        = values["lastname"];
            Username        = values["username"];
            Password        = values["password"];
            Colnum          = values["colnum"];
            Email           = values["email"];
            CreationDate    = DateTime.Parse(values["creationDate"]);
            Active          = Int32.Parse(values["active"]);
            ProfileImage    = values["profileImage"];
            LastAccess      = values["lastAccess"] != "" ? DateTime.Parse(values["lastAccess"]) : DateTime.Now;
            LastIPAdress    = values["lastIPAdress"];
            UserProfile     = new UserProfile(Int32.Parse(values["center"]), _config);
        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.User.DataLayer dl = new Leaf.Datalayers.User.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.GetById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Name = dt.Rows[0]["name"].ToString();
                    Surname = dt.Rows[0]["surname"].ToString();
                    Lastname = dt.Rows[0]["lastname"].ToString();
                    Username = dt.Rows[0]["username"].ToString();
                    Password = dt.Rows[0]["password"].ToString();
                    Colnum = dt.Rows[0]["colnum"].ToString();
                    Email = dt.Rows[0]["email"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationDate"].ToString());
                    Active = Int32.Parse(dt.Rows[0]["active"].ToString());
                    ProfileImage = dt.Rows[0]["profileImage"].ToString();
                    LastAccess = dt.Rows[0]["lastAccess"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["lastAccess"].ToString()) : DateTime.Now;
                    LastIPAdress = dt.Rows[0]["lasipaccess"].ToString();
                    UserProfile = new UserProfile(Int32.Parse(dt.Rows[0]["profileid"].ToString()), _config);
                }
                else
                {
                    throw new System.NullReferenceException("User not found");
                }
            }
        }

        private void LoadByUsername(string username)
        {
            using (Leaf.Datalayers.User.DataLayer dl = new Leaf.Datalayers.User.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.GetByUsername(username);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Name = dt.Rows[0]["name"].ToString();
                    Surname = dt.Rows[0]["surname"].ToString();
                    Lastname = dt.Rows[0]["lastname"].ToString();
                    Username = dt.Rows[0]["username"].ToString();
                    Password = dt.Rows[0]["password"].ToString();
                    Colnum = dt.Rows[0]["colnum"].ToString();
                    Email = dt.Rows[0]["email"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationDate"].ToString());
                    Active = Int32.Parse(dt.Rows[0]["active"].ToString());
                    ProfileImage = dt.Rows[0]["profileImage"].ToString();
                    LastAccess = DateTime.Parse(dt.Rows[0]["lastAccess"].ToString());
                    LastIPAdress = dt.Rows[0]["LastIPAdress"].ToString();
                    UserProfile = new UserProfile(Int32.Parse(dt.Rows[0]["Centerid"].ToString()), _config);
                }
                else
                {
                    throw new System.NullReferenceException("User not found");
                }
            }
        }

        public User Create()
        {
            using (Leaf.Datalayers.User.DataLayer dl = new Leaf.Datalayers.User.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create patient with setted NHC");
                }

                if (dl.Create(this) == 1)
                {
                    LoadByUsername(this.username);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        public User Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save patient without setted NHC");
            }

            using (Leaf.Datalayers.User.DataLayer dl = new Leaf.Datalayers.User.DataLayer(_config))
            {
                if (dl.Save(this) == 1)
                {
                    LoadById(this.Id);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        public User UpdateLastAccess()
        {
            using (Leaf.Datalayers.User.DataLayer dl = new Leaf.Datalayers.User.DataLayer(_config))
            {
                if (dl.UpdateLastAccess(this) == 1)
                {
                    LoadById(this.Id);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
