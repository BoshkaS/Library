using Library_kursova.Annotation;
using Library_kursova.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Data
{
    public class LibraryContext : IdentityDbContext<User, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        
        public LibraryContext(DbContextOptions options) : base(options)
        {

        }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            var annotationCollection = new List<IEntityAnnotation>
            {
                new AuthorAnnotation(builder),

                new BookmarkBookAnnotation(builder),
                new BorrowsBookAnnotation(builder),
                new CategoryAnnotation(builder),
                new CommentAnnotation(builder),
                new LanguageAnnotation(builder),
                new PublishingHouseAnnotation(builder),
                new UserAnnotation(builder),
                new AuthorBookAnnotation(builder),
                new BookAnnotation(builder),
                new LikedBookAnnotation(builder),
            };
            foreach (var annotation in annotationCollection)
            {
                annotation.Annotate();
            }
            base.OnModelCreating(builder);
            builder.Entity<AppRole>().HasMany(u => u.UserRoles).WithOne(u => u.Role).HasForeignKey(u => u.RoleId).IsRequired();
        }

        public DbSet<Author> Author { get; set; }

        public DbSet<AuthorBook> AuthorBook { get; set; }

        public DbSet<BookmarkBook> BookmarkBook { get; set; }

        public DbSet<BorrowsBook> BorrowsBook { get; set; }

        public DbSet<LikedBook> LikedBook {  get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Language> Language { get; set; }

        public DbSet<PublishingHouse> PublishingHouse { get; set; }

        public DbSet<Book> Book { get; set; }
        public object User { get; internal set; }
    }
}
