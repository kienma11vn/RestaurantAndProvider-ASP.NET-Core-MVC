# RestaurantAndProvider

RestaurantAndProvider là ứng dụng web mẫu quản lý thông tin nhà cung cấp và nhà hàng, được xây dựng bằng ASP.NET Core MVC. Dự án phù hợp để tham khảo cách tổ chức một ứng dụng MVC kết hợp Entity Framework Core, SQL Server và ASP.NET Core Identity.

> **Tính chất học thuật:** Đây là dự án tham khảo phục vụ học tập, thực hành và nghiên cứu. Dự án không được thiết kế, kiểm thử hay chứng nhận để vận hành như một sản phẩm production. Không sử dụng với dữ liệu thật hoặc triển khai công khai nếu chưa đánh giá, hoàn thiện và kiểm thử bảo mật.

---

## Chức năng chính

- Quản lý nhà cung cấp: xem danh sách, xem chi tiết, tạo, sửa và xóa.
- Quản lý nhà hàng: xem danh sách, xem chi tiết, tạo, sửa và xóa; mỗi nhà hàng liên kết với một nhà cung cấp.
- Tìm kiếm theo tên, sắp xếp theo tên và phân trang cho danh sách nhà hàng/nhà cung cấp.
- Có các màn hình đăng ký và đăng nhập dựa trên ASP.NET Core Identity.
- Có cấu hình vai trò `Admin` và `User`; controller có một số action yêu cầu vai trò quản trị.

---

## Công nghệ

- .NET 8 / ASP.NET Core MVC
- Entity Framework Core 8
- SQL Server; cấu hình mặc định trong `appsettings.json` sử dụng SQL Server LocalDB
- ASP.NET Core Identity
- Razor Views và Bootstrap

---

## Cấu trúc dự án

```text
.
├── RestaurantAndProvider.sln
└── RestaurantAndProvider/
    ├── Controllers/     # MVC controllers cho tài khoản, nhà hàng và nhà cung cấp
    ├── Data/            # DbContext và khởi tạo vai trò/tài khoản mẫu
    ├── Migrations/      # Các migration của Entity Framework Core
    ├── Models/          # Entity và view model
    ├── Views/           # Razor views
    └── wwwroot/         # CSS, JavaScript và thư viện phía trình duyệt
```

---

## Yêu cầu

- .NET 8 SDK
- SQL Server LocalDB trên Windows, hoặc một SQL Server khác với connection string phù hợp
- Công cụ dòng lệnh Entity Framework Core (`dotnet-ef`) để áp dụng migration

---

## Chạy ứng dụng

Từ thư mục chứa `RestaurantAndProvider.sln`:

```powershell
dotnet restore RestaurantAndProvider.sln
```

Nếu máy chưa cài `dotnet-ef`, cài phiên bản tương thích với EF Core trong dự án:

```powershell
dotnet tool install --global dotnet-ef --version 8.0.26
```

Cập nhật cơ sở dữ liệu từ các migration hiện có, sau đó chạy ứng dụng:

```powershell
dotnet ef database update --project RestaurantAndProvider/RestaurantAndProvider.csproj
dotnet run --project RestaurantAndProvider/RestaurantAndProvider.csproj
```

Ứng dụng sẽ in địa chỉ local trong terminal sau khi khởi động. Connection string mặc định dùng LocalDB; nếu dùng SQL Server khác, cập nhật `ConnectionStrings:RestaurantAndProviderContext` trong cấu hình phù hợp với môi trường của bạn.

---

## Lưu ý học thuật và bảo mật

- Tài khoản quản trị mẫu được khởi tạo trong `Data/SeedRoles.cs` bằng thông tin đăng nhập hard-code. Hãy xem và thay đổi cách khởi tạo này trước khi chia sẻ hoặc triển khai dự án; không tái sử dụng thông tin mặc định.
- Cấu hình xác thực, phân quyền (bao gồm quyền trên các action GET/POST), xử lý lỗi, kiểm tra dữ liệu và quản lý bí mật chỉ nên xem là nội dung thực hành. Cần rà soát và kiểm thử độc lập trước mọi mục đích ngoài học tập.
- Không đưa mật khẩu, connection string chứa bí mật, dữ liệu cá nhân hoặc dữ liệu nhà hàng/nhà cung cấp thật vào repository.
- Migration trong dự án phục vụ cấu trúc dữ liệu mẫu; hãy sao lưu và kiểm tra cơ sở dữ liệu trước khi áp dụng thay đổi trên dữ liệu cần giữ lại.