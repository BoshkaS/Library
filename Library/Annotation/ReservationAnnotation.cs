using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class ReservationAnnotation : BaseEntityAnnotation<Reservation>
    {
        public ReservationAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.ReservationId);
            this.ModelBuilder.Property(b => b.ReservationId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("reservation_id");
            this.ModelBuilder.Property(c => c.UserId).HasColumnName("user_id");
            this.ModelBuilder.Property(c => c.BookCopyId).HasColumnName("book_copy_id");
            this.ModelBuilder.HasOne(b => b.User).WithMany(b => b.Reservations).HasForeignKey(b => b.UserId).IsRequired(false).HasConstraintName("user_id");
            this.ModelBuilder.HasOne(b => b.BookCopy).WithMany(b => b.Reservations).HasForeignKey(b => b.BookCopyId).HasConstraintName("book_copy_id");
        }
    }
}
