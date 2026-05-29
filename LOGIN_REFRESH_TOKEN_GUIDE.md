## 📋 ระบบ Login & Register พร้อม JWT & RefreshToken - คู่มือใช้งาน

ระบบ Authentication ที่เราสร้างไปแล้วมีความสมบูรณ์เต็มร้อย ตรงตามแนว Clean Architecture 

### 📊 Flow ของระบบ

```
1. User Register ➜ สร้างบัญชี ➜ ได้ UserId
   └─ POST /api/auth/register

2. User Login ➜ ส่ง Email + Password ➜ ได้ JWT Token + RefreshToken
   └─ POST /api/auth/login

3. Access API ➜ ส่ง JWT Token ใน Header
   └─ Authorization: Bearer {accessToken}

4. Token หมดอายุ ➜ ส่ง RefreshToken ➜ ได้ Token ใหม่
   └─ POST /api/auth/refresh-token
```

---

## 🔌 API Endpoints

### 1️⃣ Register (สมัครสมาชิก)
```
POST /api/auth/register
Content-Type: application/json

{
  "name": "สมชาย",
  "email": "somchai@example.com",
  "password": "Password123"
}

Response (200 OK):
{
  "message": "สมัครสมาชิกสำเร็จ",
  "userId": 1
}
```

### 2️⃣ Login (เข้าสู่ระบบ)
```
POST /api/auth/login
Content-Type: application/json

{
  "email": "somchai@example.com",
  "password": "Password123"
}

Response (200 OK):
{
  "userId": 1,
  "name": "สมชาย",
  "email": "somchai@example.com",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "AbCdEfGhIjKlMnOpQrStUvWxYz...",
  "accessTokenExpiresAt": "2026-05-25T14:30:00Z",
  "refreshTokenExpiresAt": "2026-06-01T14:15:00Z"
}
```

### 3️⃣ Refresh Token (เรียกขอ Token ใหม่)
```
POST /api/auth/refresh-token
Content-Type: application/json

{
  "refreshToken": "AbCdEfGhIjKlMnOpQrStUvWxYz..."
}

Response (200 OK):
{
  "userId": 1,
  "name": "สมชาย",
  "email": "somchai@example.com",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "XyZ1a2b3c4d5e6f7g8h9i0j1k2l...",
  "accessTokenExpiresAt": "2026-05-25T14:45:00Z",
  "refreshTokenExpiresAt": "2026-06-08T14:30:00Z"
}
```

---

## 🛡️ ใช้ Token เพื่อเข้าถึง Protected Endpoint

```
GET /api/bookings
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

Header ต้องตรง format นี้: "Authorization: Bearer {accessToken}"
```

---

## 💾 ข้อมูลที่เก็บใน Database

User Table มีคอลัมน์ใหม่ดังนี้:
- `RefreshToken` (string) - เก็บ refresh token
- `RefreshTokenExpiresAt` (DateTime) - หมดอายุ refresh token
- `LastLoginAt` (DateTime) - เข้าสู่ระบบครั้งสุดท้าย

---

## 🚀 ขั้นตอนการติดตั้ง

### Step 1: สร้าง Migration
```bash
cd BookingSystem.Infrastructure
dotnet ef migrations add AddRefreshTokenToUser --project ../BookingSystem.Api
```

### Step 2: Update Database
```bash
dotnet ef database update --project ../BookingSystem.Api
```

### Step 3: ปรับปรุง JWT Secret Key ใน appsettings.json
```json
{
  "Jwt": {
    "SecretKey": "your-super-secret-key-change-this-in-production-at-least-32-characters-long",
    "Issuer": "BookingSystem",
    "Audience": "BookingSystemUsers",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  }
}
```

**⚠️ สำคัญ:** เปลี่ยน SecretKey ให้ยาวขึ้นและซับซ้อนขึ้นสำหรับ Production!

---

## 📝 วิธีใช้งานจาก Angular Frontend

### ตัวอย่าง Service ใน Angular:

```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5074/api/auth';

  constructor(private http: HttpClient) { }

  register(data: any) {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  login(email: string, password: string) {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password })
      .pipe(
        tap(response => {
          // เก็บ tokens ใน localStorage
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);
          localStorage.setItem('user', JSON.stringify({
            id: response.userId,
            name: response.name,
            email: response.email
          }));
        })
      );
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('user');
  }

  refreshToken() {
    const refreshToken = localStorage.getItem('refreshToken');
    return this.http.post<any>(`${this.apiUrl}/refresh-token`, { refreshToken })
      .pipe(
        tap(response => {
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);
        })
      );
  }

  getAccessToken() {
    return localStorage.getItem('accessToken');
  }
}
```

### Interceptor สำหรับแนบ Token ที่อัตโนมัติ:

```typescript
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getAccessToken();
    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }
    return next.handle(req);
  }
}
```

---

## ⚙️ Configuration Details

### Folder Structure ที่เราสร้าง:
```
BookingSystem/
├── BookingSystem.Domain/
│   └── DTOs/Users/
│       ├── LoginRequest.cs ✨ (ใหม่)
│       ├── LoginResponse.cs ✨ (ใหม่)
│       └── RefreshTokenRequest.cs ✨ (ใหม่)
├── BookingSystem.Application/
│   ├── Interfaces/
│   │   └── IAuthService.cs ✨ (ใหม่)
│   └── Services/
│       └── AuthService.cs ✨ (ใหม่)
└── BookingSystem.Api/
    ├── Program.cs (อัปเดต)
    ├── appsettings.json (อัปเดต)
    └── Controllers/
        └── AuthController.cs (อัปเดต)
```

---

## 🔐 Security Best Practices

✅ สิ่งที่ดีแล้ว:
- ใช้ JWT Tokens แทน Session
- เลือก HMAC SHA256 algorithm
- เก็บ RefreshToken ใน Database
- ตั้งเวลา Expiration
- Hash passwords ด้วย Bcrypt

⚠️ สิ่งที่ต้องทำเพิ่มเติมใน Production:

1. **เปลี่ยน SecretKey ให้ยาวขึ้น** (อย่างน้อย 32 ตัวอักษร)
```json
"SecretKey": "super-secret-key-that-is-at-least-32-characters-long-12345"
```

2. **ใช้ Environment Variables แทนการเขียนค่าลงใน appsettings.json**
```csharp
var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
```

3. **ส่ง accessToken ใน HttpOnly Cookie** (ปลอดภัยกว่า localStorage)

4. **ตรวจสอบ Token Revocation** (หากมีการ logout)

5. **ใช้ HTTPS เสมอ**

---

## 🐛 Troubleshooting

### Error: "RefreshToken ไม่ถูกต้อง"
- ตรวจสอบว่า RefreshToken ยังไม่หมดอายุ
- ตรวจสอบว่า RefreshToken ตรงกับที่เก็บใน Database

### Error: "The type initializer for 'System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler' threw an exception"
- ทำการ Clean Solution แล้ว Rebuild
- ตรวจสอบ NuGet packages: `System.IdentityModel.Tokens.Jwt`

### JWT Token ไม่ทำงาน
- ตรวจสอบว่า `app.UseAuthentication()` มาก่อน `app.MapControllers()` ใน Program.cs
- ตรวจสอบ SecretKey ตรงกันระหว่าง Program.cs กับ appsettings.json

---

## 📚 ต่อขั้นไป

เมื่อเสร็จแล้ว คุณสามารถ:
1. ✅ Protect endpoints ด้วย `[Authorize]` attribute
2. ✅ ตรวจสอบ Role ด้วย `[Authorize(Roles = "Admin")]`
3. ✅ ใช้ Token Claims เพื่อดึงข้อมูล User
4. ✅ Implement logout functionality

---

**✨ System สำเร็จแล้ว! Ready to go! ✨**
