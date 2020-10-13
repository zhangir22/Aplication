using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Account.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public string  Nickname { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string  ConfirmPassword { get; set; }
        
    }
}