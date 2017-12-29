using FreePeople.Persistence.DTO;
using Microsoft.EntityFrameworkCore;

namespace FreePeople.Persistence
{
    public class DataContext : DbContext
    {
		public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var administrator = modelBuilder.Entity<AdministratorDTO>();
			administrator.HasKey(x => x.Id);
			administrator.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(200);
			administrator.Property(x => x.Email).IsRequired().IsUnicode().HasMaxLength(200);
			administrator.ToTable("Administrator");

			var city = modelBuilder.Entity<CityDTO>();
			city.HasKey(x => x.Id);
			city.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(200);
			city.ToTable("City");

			var place = modelBuilder.Entity<PlaceDTO>();
			place.HasKey(x => x.Id);
			place.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(200);
			place.Property(x => x.Address).IsRequired().IsUnicode().HasMaxLength(2000);
			place.Property(x => x.HowToGet).IsRequired().IsUnicode().HasMaxLength(2000);
			place.Property(x => x.MapUrl).IsRequired().IsUnicode().HasMaxLength(2000);
			place.Property(x => x.ContactName).IsRequired().IsUnicode().HasMaxLength(400);
			place.Property(x => x.ContactPhone).IsRequired().HasMaxLength(50);
			place.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).IsRequired(true);
			place.ToTable("Place");

			var speaker = modelBuilder.Entity<SpeakerDTO>();
			speaker.HasKey(x => x.Id);
			speaker.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(400);
			speaker.Property(x => x.Photo).IsRequired();
			speaker.Property(x => x.About).IsRequired().IsUnicode().HasMaxLength(2000);
			speaker.Property(x => x.Email).IsRequired().IsUnicode().HasMaxLength(400);
			speaker.Property(x => x.Phone).IsRequired(false).HasMaxLength(50);
			speaker.Property(x => x.Facebook).IsRequired(false).HasMaxLength(200);
			speaker.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).IsRequired(true);
			speaker.ToTable("Speaker");

			var talk = modelBuilder.Entity<TalkDTO>();
			talk.HasKey(x => x.Id);
			talk.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(200);
			talk.Property(x => x.Comment).IsRequired().IsUnicode().HasMaxLength(400);
			talk.Property(x => x.ShortInfo).IsRequired().IsUnicode().HasMaxLength(1000);
			talk.Property(x => x.FullInfo).IsRequired().IsUnicode().HasMaxLength(2000);
			talk.Property(x => x.StartsAt).IsRequired();
			talk.Property(x => x.ApprovedAt).IsRequired(false);
			talk.HasOne(x => x.ApprovedBy).WithMany().HasForeignKey(x => x.ApprovedById).IsRequired(false);
			talk.Property(x => x.PlaceVerifiedAt).IsRequired(false);
			talk.HasOne(x => x.Place).WithMany().HasForeignKey(x => x.PlaceId).IsRequired(false);
			talk.HasOne(x => x.PlaceVerifiedBy).WithMany().HasForeignKey(x => x.PlaceVerifiedById).IsRequired(false);
			talk.Property(x => x.PublishedAt).IsRequired(false);
			talk.HasOne(x => x.PublishedBy).WithMany().HasForeignKey(x => x.PublishedById).IsRequired(false);
			talk.Property(x => x.ReportedAt).IsRequired(false);
			talk.HasOne(x => x.ReportedBy).WithMany().HasForeignKey(x => x.ReportedById).IsRequired(false);
			talk.Property(x => x.Report).IsRequired(false).IsUnicode().HasMaxLength(2000);
			talk.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).IsRequired(true);
			talk.ToTable("Talk");
		}
	}
}
