using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models.Authorization
{
    public class User : IdentityUser
    {
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime Birthday { get; set; }

    }
}