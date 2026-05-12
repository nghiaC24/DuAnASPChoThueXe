using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnASPChoThueXe.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string CustomerName { get; set; } // Dòng này quan trọng

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string CustomerPhone { get; set; } // Dòng này quan trọng

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Chờ xác nhận";

        // Liên kết với xe
        public int MotorbikeId { get; set; }
        [ForeignKey("MotorbikeId")]
        public virtual Motorbike? Motorbike { get; set; }
    }
}