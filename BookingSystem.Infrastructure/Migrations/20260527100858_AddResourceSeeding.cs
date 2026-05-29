using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeRoom = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Capacity", "Description", "RoomName", "typeRoom" },
                values: new object[,]
                {
                    { 1, 0, "ห้องประชุมหมายเลข 01 พร้อมระบบโปรเจกเตอร์", "Room 01", 0 },
                    { 2, 0, "ห้องประชุมหมายเลข 02 พร้อมระบบโปรเจกเตอร์", "Room 02", 0 },
                    { 3, 0, "ห้องประชุมหมายเลข 03 พร้อมระบบโปรเจกเตอร์", "Room 03", 0 },
                    { 4, 0, "ห้องประชุมหมายเลข 04 พร้อมระบบโปรเจกเตอร์", "Room 04", 0 },
                    { 5, 0, "ห้องประชุมหมายเลข 05 พร้อมระบบโปรเจกเตอร์", "Room 05", 0 },
                    { 6, 0, "ห้องประชุมหมายเลข 06 พร้อมระบบโปรเจกเตอร์", "Room 06", 0 },
                    { 7, 0, "ห้องประชุมหมายเลข 07 พร้อมระบบโปรเจกเตอร์", "Room 07", 0 },
                    { 8, 0, "ห้องประชุมหมายเลข 08 พร้อมระบบโปรเจกเตอร์", "Room 08", 0 },
                    { 9, 0, "ห้องประชุมหมายเลข 09 พร้อมระบบโปรเจกเตอร์", "Room 09", 0 },
                    { 10, 0, "ห้องประชุมหมายเลข 10 พร้อมระบบโปรเจกเตอร์", "Room 10", 0 },
                    { 11, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 01 (High Performance)", "DB-Server 01", 1 },
                    { 12, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 02 (High Performance)", "DB-Server 02", 1 },
                    { 13, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 03 (High Performance)", "DB-Server 03", 1 },
                    { 14, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 04 (High Performance)", "DB-Server 04", 1 },
                    { 15, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 05 (High Performance)", "DB-Server 05", 1 },
                    { 16, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 06 (High Performance)", "DB-Server 06", 1 },
                    { 17, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 07 (High Performance)", "DB-Server 07", 1 },
                    { 18, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 08 (High Performance)", "DB-Server 08", 1 },
                    { 19, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 09 (High Performance)", "DB-Server 09", 1 },
                    { 20, 0, "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 10 (High Performance)", "DB-Server 10", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
