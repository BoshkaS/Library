using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class UserRatingAnnotation : BaseEntityAnnotation<UserRatingLog>
    {
        public UserRatingAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.UserRatingId);
            this.ModelBuilder.Property(b => b.UserRatingId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("user_rating_id");
            this.ModelBuilder.Property(b => b.ChangedAt).HasColumnName("changed_at");
            this.ModelBuilder.Property(b => b.ChangeAmount).HasColumnName("change_amount");
            this.ModelBuilder.Property(b => b.Reason).HasColumnName("reason");
            this.ModelBuilder.Property(b => b.RatingAfterChange).HasColumnName("rating_after_change");
            this.ModelBuilder.Property(c => c.UserId).HasColumnName("user_id");
            this.ModelBuilder.HasOne(b => b.User).WithMany(b => b.RatingLogs).HasForeignKey(b => b.UserId).IsRequired(false).HasConstraintName("user_id");
        }
    }
}
