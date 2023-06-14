﻿using System;
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
        public int StatusId { get; set; }
        public string? Image { get; set; }
        public int? RoleId { get; set; }

        public User(string firstname, string lastName, string password, string email, bool? gender, 
            int statusId, string? image)
        {
            FirstName = firstname;
            LastName = lastName;
            Password = password;
            Email = email;
            Gender = gender;
            StatusId = statusId;
            Image = image;
        }
        public void Update(string firstname, string lastname, string password,string email, bool gender,
            int statusid,string? image, int roleId)
        {
            FirstName = firstname;
            LastName = lastname;
            Password = password;
            Email = email;
            Gender = gender;
            StatusId = statusid;
            Image = image;
            RoleId = roleId;
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
