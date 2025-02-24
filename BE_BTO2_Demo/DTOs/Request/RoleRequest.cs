using System.ComponentModel.DataAnnotations;

namespace BE_BTO2_Demo.DTOs.Request
{
    public class RoleRequest
    {
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(250, ErrorMessage = "{0} phải có độ dài từ {2} đến {1} ký tự", MinimumLength = 2)]
        public string? RoleName { get; set; }
    }
}
