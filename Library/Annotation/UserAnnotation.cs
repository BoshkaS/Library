using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class UserAnnotation : BaseEntityAnnotation<User>
    {
        public UserAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            //this.ModelBuilder.HasKey(u => u.UserId);
            //this.ModelBuilder.Property(u => u.UserId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("user_id");
            this.ModelBuilder.Property(u => u.Nickname).IsRequired().HasMaxLength(20).HasColumnName("nickname");
            this.ModelBuilder.Property(u => u.Email).IsRequired().HasMaxLength(50).HasColumnName("email");
            this.ModelBuilder.Property(u => u.PasswordHash).IsRequired().HasColumnName("password_hash");
            //this.ModelBuilder.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(50).HasColumnName("password_salt");
            this.ModelBuilder.Property(u => u.CreatedDate).IsRequired().HasColumnName("created_date");
            this.ModelBuilder.Property(u => u.ModifiedDate).HasColumnName("modified_date").IsRequired(false);
            this.ModelBuilder.Property(u => u.UserImage).HasColumnName("user_image").IsRequired(false);
            this.ModelBuilder.Property(u => u.FirstName).HasColumnName("first_name").IsRequired();
            this.ModelBuilder.Property(u => u.LastName).HasColumnName("last_name").IsRequired();
            this.ModelBuilder.Property(u => u.PhoneNumber).HasColumnName("phone_number").IsRequired();
            this.ModelBuilder.Property(u => u.IsMember).HasColumnName("is_member");
            this.ModelBuilder.Property(u => u.Rating).HasColumnName("rating");
            this.ModelBuilder.HasMany(u => u.UserRoles).WithOne(u => u.User).HasForeignKey(u => u.UserId).IsRequired();
        }
    }
}
