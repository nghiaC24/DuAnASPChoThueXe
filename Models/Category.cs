using System.ComponentModel.DataAnnotations;

namespace DuAnASPChoThueXe.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } // Ví dụ: Xe ga, Xe số, Xe côn tay

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        // Liên kết với danh sách xe
        public virtual ICollection<Motorbike>? Motorbikes { get; set; }
    }
}