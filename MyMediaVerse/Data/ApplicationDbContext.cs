using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMediaVerse.Models;

namespace MyMediaVerse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyMediaVerse.Models.Books> Books { get; set; } = default!;
        public DbSet<MyMediaVerse.Models.Articles> Articles { get; set; } = default!;
        public DbSet<MyMediaVerse.Models.Movies> Movies { get; set; } = default!;
        public DbSet<MyMediaVerse.Models.Shows> Shows { get; set; } = default!;
    }
}
