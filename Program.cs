using Microsoft.EntityFrameworkCore;
using DuAnASPChoThueXe.Data;
using DuAnASPChoThueXe.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình kết nối Database (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Thêm dịch vụ Controller với View
builder.Services.AddControllersWithViews();

// 3. Thêm Session (nếu bạn làm giỏ hàng)
builder.Services.AddSession();

var app = builder.Build();

// 4. Cấu hình Pipeline xử lý yêu cầu HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Phải đặt sau UseRouting

app.UseAuthorization();

// 5. Cấu hình Route mặc định để chạy vào trang chủ Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();