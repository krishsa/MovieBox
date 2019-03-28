using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieBox.Models;

namespace MovieBox.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<MembershipType> MembershipTypes { get; set; }
    }
}
