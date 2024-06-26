﻿using AttendanceManagement.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Core.DTO.CustomDTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string PersonName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender can't be blank")]
        [EnumDataType(typeof(GenderOptions), ErrorMessage = "Invalid gender option")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address can't be blank")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain digits only")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string DateStart { get; set; } = string.Empty;

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.Admin;
        public Guid DepartmentId { get; set; } = Guid.Empty;
    }
}
