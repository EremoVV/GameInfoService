using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentityService.Models.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityService.Contexts
{
    public class AuthorizationContext : IdentityDbContext<User>
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            :base(options)
        {

        }
    }
}
