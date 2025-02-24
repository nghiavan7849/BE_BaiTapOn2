using BE_BTO2_Demo.Models;
using System.Text.Json.Serialization;

namespace BE_BTO2_Demo.DTOs.Response
{
    public class AllowAccessResponse
    {
        public int Id { get; set; }
        public string? TableName { get; set; }
        public string? AccessProperties { get; set; }
        public int RoleId { get; set; }
    }
}
