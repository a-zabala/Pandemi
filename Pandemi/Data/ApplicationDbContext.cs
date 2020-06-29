using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pandemi.Models;

namespace Pandemi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }

        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Accomplishment> Accomplishments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Food> Foods { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }
       

    }
}
