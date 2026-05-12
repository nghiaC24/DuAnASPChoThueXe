using Microsoft.AspNetCore.Mvc;

namespace DuAnASPChoThueXe.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            // Tạo danh sách các dịch vụ tại các quận TP.HCM để hiển thị ra giao diện
            var hcmServices = new List<ServiceVM>
            {
                new ServiceVM { Id = 1, Title = "Cho thuê xe máy Quận 1 - Giao xe tận khách sạn", Description = "Dịch vụ cho thuê xe máy tại Quận 1 giá rẻ, thủ tục nhanh gọn, giao xe tận nơi chỉ trong 15 phút...", ImageUrl = "https://media.vneconomy.vn/images/upload/2022/05/23/xe-may.jpg", Date = "10/05/2024" },
                new ServiceVM { Id = 2, Title = "Thuê xe máy tại Quận Tân Bình - Gần sân bay Tân Sơn Nhất", Description = "Khách du lịch đáp sân bay có nhu cầu thuê xe máy di chuyển vào trung tâm, hãy gọi ngay KNP Rental...", ImageUrl = "https://lh3.googleusercontent.com/p/AF1QipP_L1v3oX6lJp_Lp_Lp_Lp", Date = "09/05/2024" },
                new ServiceVM { Id = 3, Title = "Dịch vụ thuê xe máy Quận 7 - Phú Mỹ Hưng giá ưu đãi", Description = "Cung cấp các dòng xe tay ga đời mới như Vision, SH Mode cho khách hàng tại khu vực Quận 7...", ImageUrl = "https://statics.vinpearl.com/thue-xe-may-sai-gon-1_1631006456.jpg", Date = "08/05/2024" },
                new ServiceVM { Id = 4, Title = "Cho thuê xe máy Quận Bình Thạnh - Giao xe tận nhà", Description = "Bạn cần xe đi làm hoặc đi phượt, chúng tôi hỗ trợ giao xe tận nhà tại khu vực Bình Thạnh...", ImageUrl = "https://motorbikesharing.com/wp-content/uploads/2020/01/thue-xe-may-tphcm.jpg", Date = "07/05/2024" }
            };

            return View(hcmServices);
        }
    }

    // Lớp tạm để chứa dữ liệu dịch vụ
    public class ServiceVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
    }
}