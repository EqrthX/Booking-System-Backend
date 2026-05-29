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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Booking>()
                .Property(b => b.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            var seedData = new List<Resource>();

            // 1. วนลูปเสกห้องประชุม 10 ห้อง (ID 1 - 10)
            for (int i = 1; i <= 10; i++)
            {
                seedData.Add(new Resource
                {
                    Id = i,
                    RoomName = $"Room {i:D2}",
                    typeRoom = TypeRoom.Room,
                    Description = $"ห้องประชุมหมายเลข {i:D2} พร้อมระบบโปรเจกเตอร์"
                });
            }

            for (int i = 1; i <= 10; i++)
            {
                seedData.Add(new Resource
                {
                    Id = i + 10,
                    RoomName = $"DB-Server {i:D2}",
                    typeRoom = TypeRoom.Server,
                    Description = $"เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ {i:D2} (High Performance)"
                });
            }

            // สั่งให้ EF Core มัดรวมข้อมูลชุดนี้ไปสร้างในตารางตอนทำ Migration
            modelBuilder.Entity<Resource>().HasData(seedData);
        }
    }
}
