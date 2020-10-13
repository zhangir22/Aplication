using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Account.Models
{
    public class NicknameChange
    {
        [Display(Name ="Old name")]
        public string OldName { get; set; }
        [Display(Name ="New name")]
        public string NewName { get; set; }
    }
}