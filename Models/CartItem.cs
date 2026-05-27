using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnASPChoThueXe.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        // 1. QUAN TRỌNG: Sửa MotorbikeId thành kiểu int? (nullable) 
        // để một món hàng có thể không cần có xe máy (nếu nó là ô tô)
        public int? MotorbikeId { get; set; }

        [ForeignKey("MotorbikeId")]
        public virtual Motorbike? Motorbike { get; set; }
        public string? LicensePlate { get; set; }

        // 2. THÊM MỚI: Liên kết với bảng Ô tô
        public int? CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car? Car { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Giá thuê tạm tính")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? CartId { get; set; }
    }
}