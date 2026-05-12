using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnASPChoThueXe.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        // ID của xe máy được thêm vào giỏ
        public int MotorbikeId { get; set; }

        [ForeignKey("MotorbikeId")]
        public virtual Motorbike? Motorbike { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Giá thuê tạm tính")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Thời gian thuê dự kiến (nếu cần lưu trong giỏ hàng)
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // ID của Session hoặc User để phân biệt giỏ hàng của từng người
        public string? CartId { get; set; }
    }
}