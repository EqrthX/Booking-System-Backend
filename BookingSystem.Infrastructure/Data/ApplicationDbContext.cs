using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace BookingSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceDetail> ResourceDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Microsoft.EntityFrameworkCore.Diagnostics.PendingModelChangesWarning", "Ignore");
            // Configure Booking relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Resource)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Resource and ResourceDetail (1:1)
            modelBuilder.Entity<Resource>()
                .HasOne(r => r.ResourceDetail)
                .WithOne(rd => rd.Resource)
                .HasForeignKey<ResourceDetail>(rd => rd.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ResourceDetail decimal properties
            modelBuilder.Entity<ResourceDetail>()
                .Property(rd => rd.PricePerHour)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ResourceDetail>()
                .Property(rd => rd.PricePerDay)
                .HasPrecision(10, 2);

            var resourcesSeed = new List<Resource>();
            var resourceDetailsSeed = new List<ResourceDetail>();

            // 💡 ท่าไม้ตาย: ล็อกค่าเวลาให้เป็นค่าคงที่ (Static) ไว้ตรงนี้ที่เดียว!
            var seedTime = new DateTime(2026, 5, 29, 0, 0, 0, DateTimeKind.Utc);

            // 1. วนลูปเสกห้องประชุม 10 ห้อง (ID: 1 - 10)
            for (int i = 1; i <= 10; i++)
            {
                resourcesSeed.Add(new Resource { Id = i, RoomName = $"Meeting Room {i:D2}", typeRoom = TypeRoom.Room });

                var roomDetail = new ResourceDetail(resourceId: i)
                {
                    Id = i,
                    Features = i <= 5 ? "Smart TV 55\", Whiteboard" : "Projector 4K, Sound System",
                    Amenities = "Free Coffee & Water, Air Conditioner",
                    RoomImages = $"[\"assets/images/rooms/room-{i}.jpg\"]",
                    Location = $"ชั้นที่ {((i - 1) / 4) + 1} โซน A อาคาร HQ",
                    PricePerHour = 150.00m + (i * 20m),
                    PricePerDay = 1200.00m + (i * 100m),
                    ContactPerson = "Admin Team A",
                    PhoneNumber = "02-123-4567",
                    IsActive = true,
                    LastUpdated = seedTime // ❌ จากเดิม DateTime.UtcNow ->  เปลี่ยนเป็น seedTime ตัวแปรคงที่แทน
                };
                resourceDetailsSeed.Add(roomDetail);
            }

            // 2. วนลูปเสกเซิร์ฟเวอร์ 10 เครื่อง (ID: 11 - 20)
            for (int i = 1; i <= 10; i++)
            {
                int resId = i + 10;
                resourcesSeed.Add(new Resource { Id = resId, RoomName = $"DB-Server Cluster {i:D2}", typeRoom = TypeRoom.Server });

                var serverDetail = new ResourceDetail(resourceId: resId)
                {
                    Id = resId,
                    Features = i <= 5 ? "vCPU: 16 Cores, RAM: 64GB" : "vCPU: 64 Cores, RAM: 256GB",
                    Amenities = "Uptime 99.99%, Automated Backup",
                    RoomImages = $"[\"assets/images/servers/server-{i}.jpg\"]",
                    Location = $"ตู้ Rack เลขที่ {i:D2} ห้อง DataCenter ชั้น 5",
                    PricePerHour = i <= 5 ? 80.00m : 250.00m,
                    PricePerDay = i <= 5 ? 650.00m : 1800.00m,
                    ContactPerson = "DevOps Team",
                    PhoneNumber = "02-999-8888",
                    IsActive = true,
                    LastUpdated = seedTime // ❌ จากเดิม DateTime.UtcNow ->  เปลี่ยนเป็น seedTime ตัวแปรคงที่แทน
                };
                resourceDetailsSeed.Add(serverDetail);
            }

            modelBuilder.Entity<Resource>().HasData(resourcesSeed);
            modelBuilder.Entity<ResourceDetail>().HasData(resourceDetailsSeed);
        }
    }
}
