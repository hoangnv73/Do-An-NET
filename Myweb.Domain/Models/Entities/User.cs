using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myweb.Domain.Models.Entities
{
    public class User: EntityBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool? Gender { get; set; }

        /// <summary>
        /// StatusId = 1: pending
        /// </summary>
        public int StatusId { get; set; }
        public string? Image { get; set; }
        public int? RoleId { get; set; }
        public int? CheckLogin { get; set; }
        public DateTime? LastLogin { get; set; }

        public User(string firstname, string lastName, string password, string email, bool? gender, 
            int statusId, string? image, int checkLogin)
        {
            FirstName = firstname;
            LastName = lastName;
            Password = password;
            Email = email;
            Gender = gender;
            StatusId = statusId;
            Image = image;
            CheckLogin = checkLogin;
            LastLogin = DateTime.Now;

        }
        public void Update(string firstname, string lastname, string password, bool gender,
            int statusid,string? image)
        {
            FirstName = firstname;
            LastName = lastname;
            Password = password;
            Gender = gender;
            StatusId = statusid;
            Image = image;
        }

        public void Update(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
        public void Update( string password)
        {
           Password = password;
        }
        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
        }
        public User()
        {

        }
    }

    
}
