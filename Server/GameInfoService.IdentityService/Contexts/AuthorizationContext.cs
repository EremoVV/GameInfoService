using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameInfoService.IdentityService.Models.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameInfoService.IdentityService.Contexts
{
    public class AuthorizationContext : IdentityDbContext<UserEntity>
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            :base(options)
        {

        }
    }
}
