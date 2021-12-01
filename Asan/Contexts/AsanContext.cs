using Asan.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Contexts
{
    public class AsanContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public AsanContext(DbContextOptions<AsanContext> options)
            :base(options)
        {
            bool st = Database.EnsureCreated();
        }
    }
}
