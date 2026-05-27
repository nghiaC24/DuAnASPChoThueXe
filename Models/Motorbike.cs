using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnASPChoThueXe.Models
{
    public class Motorbike
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên xe máy là bắt buộc")]
        [Display(Name = "Tên xe máy")]
        public string Name { get; set; } = "";

        // Thêm dấu ? để cho phép NULL trong Database nếu lỡ quên nhập
        [Display(Name = "Biển số xe")]
        public string? LicensePlate { get; set; }

        [Required]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Display(Name = "Phân khối")]
        public string? Capacity { get; set; }

        [Required]
        [Display(Name = "Giá thuê/Ngày")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; }

        // SỬA TẠI ĐÂY: Thêm dấu ? để không bị lỗi "Cannot insert NULL"
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Gợi ý")]
        public string? Suggestion { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Sẵn sàng";

        [Display(Name = "Tổng số lượng")]
        public int TotalQuantity { get; set; } = 1;

        [Display(Name = "Số lượng còn")]
        public int AvailableQuantity { get; set; } = 1;

        public double? CurrentLat { get; set; }
        public double? CurrentLng { get; set; }
    }
}