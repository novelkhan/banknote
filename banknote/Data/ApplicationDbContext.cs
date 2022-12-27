using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using banknote.Models;

namespace banknote.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<banknote.Models.Person>? Person { get; set; }
        //public DbSet<banknote.Models.Note>? Note { get; set; }

        public DbSet<banknote.Models.User>? Users { get; set; }
        public DbSet<banknote.Models.Picture>? Pictures { get; set; }
        public DbSet<banknote.Data.Books> Books { get; set; }
        public DbSet<banknote.Data.Language> Language { get; set; }
        public DbSet<banknote.Data.BookGallery> BookGallery { get; set; }
    }
}