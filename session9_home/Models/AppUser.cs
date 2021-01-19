using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session9_home.Models
{
    public class AppUser:IdentityUser
    {

    }
    public class IdentityContext:IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions options):base(options)
        {

        }
    }
}
