using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CachingMetRedis.Models
{
    public partial class AirBNBRedisContext : DbContext
    {
        public AirBNBRedisContext()
        {
        }

        public AirBNBRedisContext(DbContextOptions<AirBNBRedisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SummaryListings> SummaryListings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<SummaryListings>(entity =>
            {
                entity.ToTable("summary-listings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Availability365).HasColumnName("availability_365");

                entity.Property(e => e.CalculatedHostListingsCount).HasColumnName("calculated_host_listings_count");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.HostName).HasColumnName("host_name");

                entity.Property(e => e.LastReview).HasColumnName("last_review");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Neighbourhood).HasColumnName("neighbourhood");

                entity.Property(e => e.NeighbourhoodGroup)
                    .HasColumnName("neighbourhood_group")
                    .HasMaxLength(1);

                entity.Property(e => e.NumberOfReviews).HasColumnName("number_of_reviews");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReviewsPerMonth).HasColumnName("reviews_per_month");

                entity.Property(e => e.RoomType).HasColumnName("room_type");
            });
        }
    }
}
