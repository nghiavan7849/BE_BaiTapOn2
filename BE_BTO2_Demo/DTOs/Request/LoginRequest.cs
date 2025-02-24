using System.ComponentModel.DataAnnotations;

namespace BE_BTO2_Demo.DTOs.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "{0} không được để trống")]
        [EmailAddress(ErrorMessage = "{0} sai định dạng")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(250, ErrorMessage = "{0} phải có độ dài từ {2} đến {1} ký tự", MinimumLength = 5)]
        public string? Password { get; set; }
    }
}
