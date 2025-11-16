using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketHive.Models;

namespace TicketHive.Data
{
    public class TicketHiveContext : DbContext
    {
        public TicketHiveContext (DbContextOptions<TicketHiveContext> options)
            : base(options)
        {
        }

        public DbSet<TicketHive.Models.Concert> Concert { get; set; } = default!;
        public DbSet<TicketHive.Models.Category> Category { get; set; } = default!;
        public DbSet<TicketHive.Models.Owner> Owner { get; set; } = default!;
        public DbSet<TicketHive.Models.Purchase> Purchase { get; set; } = default!;
    }
}
