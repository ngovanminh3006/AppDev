﻿using System.ComponentModel.DataAnnotations;

namespace AppDev.Areas.Admin.ViewModels
{
    public class ResetPasswordViewModel
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
