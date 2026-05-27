using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnASPChoThueXe.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên xe không được để trống")]
        public string Name { get; set; }

        public string Brand { get; set; } // Thương hiệu (Toyota, Honda...)
        public string LicensePlate { get; set; }

        [Required]
        public int Seats { get; set; } // Số chỗ ngồi (4, 5, 7) - Rất quan trọng để lọc xe

        public string Transmission { get; set; } // Số sàn / Số tự động

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; } // Giá thuê theo ngày

        public string ImageUrl { get; set; } // Đường dẫn ảnh xe

        public string Description { get; set; } // Mô tả thêm

        public bool IsAvailable { get; set; } = true; // Trạng thái xe (Còn trống hay không)

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Sẵn sàng"; // Sẵn sàng, Đang thuê, Bảo trì

        // 2. Số lượng (Nếu bạn quản lý theo dòng xe)
        [Display(Name = "Tổng số lượng")]
        public int TotalQuantity { get; set; } = 1;

        [Display(Name = "Số lượng còn")]
        public int AvailableQuantity { get; set; } = 1;

        // 3. Vị trí GPS (Toạ độ)
        public double? CurrentLat { get; set; } // Vĩ độ
        public double? CurrentLng { get; set; } // Kinh độ
    }
}