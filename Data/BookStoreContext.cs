using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace seminar.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }
        #region DbSet
        public DbSet<Book>? Books { get; set; }
        public DbSet<Pen>? Pens { get; set; }
        #endregion
        
    }
}
