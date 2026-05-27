using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnASPChoThueXe.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string? CustomerPhone { get; set; }
        [Required]
        public string? CitizenId { get; set; }
        public string? Province { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Chờ xác nhận";

        // ==========================================
        // PHẦN QUẢN LÝ THUÊ XE (ĐÃ CẬP NHẬT)
        // ==========================================

        // 1. Quản lý thuê Xe Máy (Cho phép null nếu khách thuê ô tô)
        public int? MotorbikeId { get; set; }
        [ForeignKey("MotorbikeId")]
        public virtual Motorbike? Motorbike { get; set; }

        // 2. Quản lý thuê Ô Tô (Cho phép null nếu khách thuê xe máy)
        public int? CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual Car? Car { get; set; }

        // 3. Ngày trả xe thực tế 
        // Nếu null = Xe chưa trả (Đang thuê)
        // Nếu có giá trị = Xe đã trả vào ngày này
        public DateTime? ActualReturnDate { get; set; }

        // ==========================================
    }
}