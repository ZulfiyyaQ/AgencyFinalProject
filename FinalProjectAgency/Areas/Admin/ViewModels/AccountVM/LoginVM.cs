﻿using System.ComponentModel.DataAnnotations;

namespace FinalProjectAgency.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]

        [DataType(DataType.Password)]

        public string Password { get; set; }

        public bool IsRemembered { get; set; }
    }
}
