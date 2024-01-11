using Services.Service.CMS.Service.CMS.ManageBlog.Module;
using System.Text;
namespace Services;
public static class Constant
{
    public static string GPTGenerateBlogPromt(BlogDtoForSource dto)
    {
        StringBuilder promt = new StringBuilder();
        promt.AppendLine("Viết lại nội dung của bài viết này bằng tiếng Việt, Chuẩn SEO sau đó đưa cho tôi HTML, không cần giải thích gì thêm.");
        promt.AppendLine("Dựa theo các ý sau đây:");
        promt.AppendLine("- Mở đầu bằng một đoạn văn để thu húc người đọc tiếp tục đọc.");
        promt.AppendLine("- Tưởng tượng đang viết cho một người không hiểu gì đọc, hãy viết dễ hiểu nhất có thể.");
        promt.AppendLine("- Giọng văn tiêu chuẩn - giữ nguyên ý nghĩa.");
        promt.AppendLine("- Bài viết có độ dài khoản 1000 - 1500 từ.");
        promt.AppendLine("- Viết thêm các heading vào bài viết.");
        promt.AppendLine("- Giữ nguyên các từ ngữ chuyên môn.");
        promt.AppendLine($"- Đặt tiêu đề H1 gây tò mò, chuẩn SEO có độ dài từ 50 - 60 kí tự, khiến người dùng phải click vào đọc, dựa theo tiêu đề sau: '{dto.Title}'");
        promt.AppendLine("- Tạo thêm các thẻ <img> ngay dưới các thẻ <h2>.");
        promt.AppendLine("- Tạo thêm thẻ meta description chuẩn SEO có độ dài từ 150 - 160 kí tự.");
        promt.AppendLine("- Tạo thêm thẻ meta url chuẩn SEO có độ dài nhỏ hơn 60 kí tự.");
        promt.AppendLine("- Phần nội dung chính sẽ được đặt bên trong thẻ <article>.");
        promt.AppendLine("- Thẻ <h1> đặt ở ngoài thẻ <article>.");
        promt.AppendLine("- Chú ý các lỗi chính tả.");
        promt.AppendLine("- Thêm danh sách các danh mục vào thẻ <ul> có id là category, chỉ bao gồm 2 thẻ <li>.");
        promt.AppendLine("- Thêm danh sách các hashtag hoặc topic bằng tiếng việt mà bài viết này có thể gắn vào thẻ <ul> có id là hastag");
        promt.AppendLine("- Thêm danh sách các keyword ở trong bài viết");

        return promt.ToString();
    }

    public static string GPTGenerateBlogContentImage(string blogTitle, string subTitle)
    {
        StringBuilder promt = new StringBuilder();
        promt.AppendLine($"Tôi đang tạo ảnh cho bài viết \"{blogTitle}\":");
        promt.AppendLine($"- Bức ảnh mô tả: \"{subTitle}\"");
        promt.AppendLine($"- kích thước 1792x1024");
        promt.AppendLine($"- định dạng JPEG");
        promt.AppendLine($"Đưa cho tôi bức ảnh, không cần giải thích gì thêm!");

        return promt.ToString();
    }

    public static string GPTGenerateBlogAvatarImage(string blogTitle, string blogDescription)
    {
        StringBuilder promt = new StringBuilder();
        promt.AppendLine($"Tôi đang tạo ảnh cho bài viết có tiêu đề là \"{blogTitle}\":");
        promt.AppendLine($"- Bức ảnh mô tả: \"{blogDescription}\"");
        promt.AppendLine($"- kích thước 1792x1024");
        promt.AppendLine($"- định dạng JPEG");
        promt.AppendLine($"Đưa cho tôi bức ảnh, không cần giải thích gì thêm!");

        return promt.ToString();
    }
}
