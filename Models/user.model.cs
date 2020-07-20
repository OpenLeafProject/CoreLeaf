using System;
using System.Collections.Generic;
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
        private string lastIPAddress;
        private UserProfile userProfile;

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
        public string LastIPAddress { get => lastIPAddress; set => lastIPAddress = value; }
        public UserProfile UserProfile { get => userProfile; set => userProfile = value; }
    }
}
