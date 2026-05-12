using DuAnASPChoThueXe.Models; // Đảm bảo namespace này khớp với thư mục Models của bạn
using Microsoft.EntityFrameworkCore;

namespace DuAnASPChoThueXe.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor này dùng để nhận chuỗi kết nối từ file Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Khai báo các bảng dữ liệu (Table) trong SQL Server

        // 1. Danh mục xe (Xe ga, Xe số)
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        // 2. Danh sách xe máy
        public DbSet<Motorbike> Motorbikes { get; set; }

        // 3. Tài khoản người dùng/thành viên
        public DbSet<User> Users { get; set; }

        // 4. Đơn đặt thuê xe (Giỏ hàng)
        public DbSet<Booking> Bookings { get; set; }

        // 5. Các câu hỏi thường gặp
        public DbSet<FAQ> FAQs { get; set; }

        // Nếu bạn có thêm model ServiceInfo thì hãy bỏ comment dòng dưới
        // public DbSet<ServiceInfo> Services { get; set; }
    }
}