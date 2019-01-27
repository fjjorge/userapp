using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUsers.Models
{
    public class WebUserContext : DbContext
    {
        public WebUserContext(DbContextOptions<WebUserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
