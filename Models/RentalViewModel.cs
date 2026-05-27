
namespace DuAnASPChoThueXe.Models
{
    public class RentalViewModel
    {
        public int IdDonThue { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string TenXe { get; set; }
        public string BienSoXe { get; set; }
        public DateTime NgayThue { get; set; }
        public DateTime? NgayTraThucTe { get; set; }
        public bool DaTraXe => NgayTraThucTe.HasValue;
        public bool IsReturned => NgayTraThucTe.HasValue;
    }
}