﻿using System.ComponentModel.DataAnnotations;

namespace MyWeb2023.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
