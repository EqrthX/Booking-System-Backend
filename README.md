# 🖥️ Enterprise Resource Booking System - Backend API

ระบบหลังบ้าน (Backend API) สำหรับโครงการ **Enterprise Resource Booking System** พัฒนาด้วยภาษา C# .NET Core โดยออกแบบตามหลักสถาปัตยกรรม **Clean Architecture** เพื่อรองรับการทำธุรกรรมจองทรัพยากรขององค์กรแบบ Real-time เช่น ห้องประชุม และ เซิร์ฟเวอร์ฐานข้อมูล

> ⚠️ **คำเตือนในการใช้งาน:** Repository นี้เป็นเพียงระบบหลังบ้าน (API) เท่านั้น จำเป็นต้องใช้งานร่วมกับระบบหน้าบ้าน (Frontend) สามารถเข้าถึงซอร์สโค้ดฝั่งหน้าบ้านได้ที่:
> 🔗 **Frontend Repository:** [https://github.com/EqrthX/Booking-System-Frontend](https://github.com/EqrthX/Booking-System-Frontend)

---

## 🚀 คุณสมบัติเด่น (Core Features)
* **Clean Architecture Layout:** แยกโครงสร้างโปรเจกต์ออกเป็น Domain, Application, Infrastructure และ Web API อย่างชัดเจน ง่ายต่อการขยายระบบ
* **Data Seeding System:** มีระบบจัดเตรียมข้อมูลเริ่มต้นอัตโนมัติ (เสกห้องประชุม 10 ห้อง และ Database Server 10 เครื่อง) ทันทีที่รันคำสั่ง Migration
* **Cloud Ready:** ตั้งค่าระบบพร้อมเชื่อมต่อกับฐานข้อมูล Azure SQL Database
* **Real-time Synchronization Ready:** โครงสร้างรองรับการเชื่อมต่อแบบเรียลไทม์เพื่อซิงค์ข้อมูลสถานะทรัพยากรไปยังหน้าบ้าน

---

## 🛠️ เทคโนโลยีที่เลือกใช้ (Tech Stack)
* **Language/Framework:** C# .NET Core (Web API)
* **ORM:** Entity Framework Core (EF Core)
* **Database:** SQL Server / Azure SQL Database

---

## ⚙️ ขั้นตอนการติดตั้งและเริ่มใช้งาน (Getting Started)

1. **Clone Repositoryนี้ลงเครื่อง:**
```bash
   git clone [https://github.com/EqrthX/Booking-System-Backend.git](https://github.com/EqrthX/Booking-System-Backend.git)
   cd Booking-System-Backend
```
2. **ดาวน์โหลดไลบรารีและ Dependencies (NuGet Restore)
```bash
   dotnet restore
```
3. **อัปเดตโครงสร้างตารางและข้อมูลเริ่มต้น (Data Seeding) เข้าสู่ Database
```bash
   dotnet ef database update --project BookingSystem.Infrastructure --startup-project BookingSystem.Api
```   
4. **เริ่มรันระบบ API

```bash
   dotnet run --project BookingSystem.Api
```
