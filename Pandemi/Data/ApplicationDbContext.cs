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
        
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
