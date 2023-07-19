using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Amul.Models.DTO
{
    public class RegisterRequestGetDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
