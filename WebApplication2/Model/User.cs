﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPwd { get; set; } 
    }
}
