using System.ComponentModel.DataAnnotations;

namespace DuAnASPChoThueXe.Models
{
    public class FAQ
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}