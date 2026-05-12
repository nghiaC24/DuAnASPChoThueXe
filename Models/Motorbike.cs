using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnASPChoThueXe.Models
{
    public class Motorbike
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên xe máy")]
        public string Name { get; set; } = "";

        [Required]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Display(Name = "Phân khối")]
        public string Capacity { get; set; } = "";

        [Required]
        [Display(Name = "Giá thuê/Ngày")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; } = "";

        [Display(Name = "Hình ảnh")]
        public string ImageUrl { get; set; } = "";

        [Display(Name = "Gợi ý")]
        public string Suggestion { get; set; } = "";
    }
}