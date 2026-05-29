using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResourceSeedingFinalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "TimeSlot",
                table: "Bookings",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Resources",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Resources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Resources",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledAt",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Bookings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResourceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Amenities = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RoomImages = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PricePerHour = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceDetails_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ResourceDetails",
                columns: new[] { "Id", "Amenities", "ContactPerson", "Features", "IsActive", "LastUpdated", "Location", "PhoneNumber", "PricePerDay", "PricePerHour", "ResourceId", "RoomImages" },
                values: new object[,]
                {
                    { 1, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Smart TV 55\", Whiteboard", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 1 โซน A อาคาร HQ", "02-123-4567", 1300.00m, 170.00m, 1, "[\"assets/images/rooms/room-1.jpg\"]" },
                    { 2, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Smart TV 55\", Whiteboard", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 1 โซน A อาคาร HQ", "02-123-4567", 1400.00m, 190.00m, 2, "[\"assets/images/rooms/room-2.jpg\"]" },
                    { 3, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Smart TV 55\", Whiteboard", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 1 โซน A อาคาร HQ", "02-123-4567", 1500.00m, 210.00m, 3, "[\"assets/images/rooms/room-3.jpg\"]" },
                    { 4, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Smart TV 55\", Whiteboard", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 1 โซน A อาคาร HQ", "02-123-4567", 1600.00m, 230.00m, 4, "[\"assets/images/rooms/room-4.jpg\"]" },
                    { 5, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Smart TV 55\", Whiteboard", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 2 โซน A อาคาร HQ", "02-123-4567", 1700.00m, 250.00m, 5, "[\"assets/images/rooms/room-5.jpg\"]" },
                    { 6, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Projector 4K, Sound System", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 2 โซน A อาคาร HQ", "02-123-4567", 1800.00m, 270.00m, 6, "[\"assets/images/rooms/room-6.jpg\"]" },
                    { 7, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Projector 4K, Sound System", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 2 โซน A อาคาร HQ", "02-123-4567", 1900.00m, 290.00m, 7, "[\"assets/images/rooms/room-7.jpg\"]" },
                    { 8, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Projector 4K, Sound System", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 2 โซน A อาคาร HQ", "02-123-4567", 2000.00m, 310.00m, 8, "[\"assets/images/rooms/room-8.jpg\"]" },
                    { 9, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Projector 4K, Sound System", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 3 โซน A อาคาร HQ", "02-123-4567", 2100.00m, 330.00m, 9, "[\"assets/images/rooms/room-9.jpg\"]" },
                    { 10, "Free Coffee & Water, Air Conditioner", "Admin Team A", "Projector 4K, Sound System", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ชั้นที่ 3 โซน A อาคาร HQ", "02-123-4567", 2200.00m, 350.00m, 10, "[\"assets/images/rooms/room-10.jpg\"]" },
                    { 11, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 16 Cores, RAM: 64GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 01 ห้อง DataCenter ชั้น 5", "02-999-8888", 650.00m, 80.00m, 11, "[\"assets/images/servers/server-1.jpg\"]" },
                    { 12, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 16 Cores, RAM: 64GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 02 ห้อง DataCenter ชั้น 5", "02-999-8888", 650.00m, 80.00m, 12, "[\"assets/images/servers/server-2.jpg\"]" },
                    { 13, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 16 Cores, RAM: 64GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 03 ห้อง DataCenter ชั้น 5", "02-999-8888", 650.00m, 80.00m, 13, "[\"assets/images/servers/server-3.jpg\"]" },
                    { 14, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 16 Cores, RAM: 64GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 04 ห้อง DataCenter ชั้น 5", "02-999-8888", 650.00m, 80.00m, 14, "[\"assets/images/servers/server-4.jpg\"]" },
                    { 15, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 16 Cores, RAM: 64GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 05 ห้อง DataCenter ชั้น 5", "02-999-8888", 650.00m, 80.00m, 15, "[\"assets/images/servers/server-5.jpg\"]" },
                    { 16, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 64 Cores, RAM: 256GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 06 ห้อง DataCenter ชั้น 5", "02-999-8888", 1800.00m, 250.00m, 16, "[\"assets/images/servers/server-6.jpg\"]" },
                    { 17, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 64 Cores, RAM: 256GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 07 ห้อง DataCenter ชั้น 5", "02-999-8888", 1800.00m, 250.00m, 17, "[\"assets/images/servers/server-7.jpg\"]" },
                    { 18, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 64 Cores, RAM: 256GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 08 ห้อง DataCenter ชั้น 5", "02-999-8888", 1800.00m, 250.00m, 18, "[\"assets/images/servers/server-8.jpg\"]" },
                    { 19, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 64 Cores, RAM: 256GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 09 ห้อง DataCenter ชั้น 5", "02-999-8888", 1800.00m, 250.00m, 19, "[\"assets/images/servers/server-9.jpg\"]" },
                    { 20, "Uptime 99.99%, Automated Backup", "DevOps Team", "vCPU: 64 Cores, RAM: 256GB", true, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "ตู้ Rack เลขที่ 10 ห้อง DataCenter ชั้น 5", "02-999-8888", 1800.00m, 250.00m, 20, "[\"assets/images/servers/server-10.jpg\"]" }
                });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 208, DateTimeKind.Utc).AddTicks(2711), "", "Meeting Room 01" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4121), "", "Meeting Room 02" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4174), "", "Meeting Room 03" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4212), "", "Meeting Room 04" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4216), "", "Meeting Room 05" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4232), "", "Meeting Room 06" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4241), "", "Meeting Room 07" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4258), "", "Meeting Room 08" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4269), "", "Meeting Room 09" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4274), "", "Meeting Room 10" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4476), "", "DB-Server Cluster 01" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4485), "", "DB-Server Cluster 02" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4487), "", "DB-Server Cluster 03" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4491), "", "DB-Server Cluster 04" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4493), "", "DB-Server Cluster 05" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4496), "", "DB-Server Cluster 06" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4504), "", "DB-Server Cluster 07" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4509), "", "DB-Server Cluster 08" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4519), "", "DB-Server Cluster 09" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedDate", "Description", "RoomName" },
                values: new object[] { new DateTime(2026, 5, 29, 7, 33, 8, 209, DateTimeKind.Utc).AddTicks(4522), "", "DB-Server Cluster 10" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ResourceId",
                table: "Bookings",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceDetails_ResourceId",
                table: "ResourceDetails",
                column: "ResourceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Resources_ResourceId",
                table: "Bookings",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Resources_ResourceId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "ResourceDetails");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ResourceId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckInTime",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckOutTime",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ConfirmedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Bookings",
                newName: "TimeSlot");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Bookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 01 พร้อมระบบโปรเจกเตอร์", "Room 01" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 02 พร้อมระบบโปรเจกเตอร์", "Room 02" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 03 พร้อมระบบโปรเจกเตอร์", "Room 03" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 04 พร้อมระบบโปรเจกเตอร์", "Room 04" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 05 พร้อมระบบโปรเจกเตอร์", "Room 05" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 06 พร้อมระบบโปรเจกเตอร์", "Room 06" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 07 พร้อมระบบโปรเจกเตอร์", "Room 07" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 08 พร้อมระบบโปรเจกเตอร์", "Room 08" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 09 พร้อมระบบโปรเจกเตอร์", "Room 09" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "ห้องประชุมหมายเลข 10 พร้อมระบบโปรเจกเตอร์", "Room 10" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 01 (High Performance)", "DB-Server 01" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 02 (High Performance)", "DB-Server 02" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 03 (High Performance)", "DB-Server 03" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 04 (High Performance)", "DB-Server 04" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 05 (High Performance)", "DB-Server 05" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 06 (High Performance)", "DB-Server 06" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 07 (High Performance)", "DB-Server 07" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 08 (High Performance)", "DB-Server 08" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 09 (High Performance)", "DB-Server 09" });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "RoomName" },
                values: new object[] { "เซิร์ฟเวอร์ฐานข้อมูลเครื่องที่ 10 (High Performance)", "DB-Server 10" });
        }
    }
}
