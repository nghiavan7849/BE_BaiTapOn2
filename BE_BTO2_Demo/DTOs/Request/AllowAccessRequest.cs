namespace BE_BTO2_Demo.DTOs.Request
{
    public class AllowAccessRequest
    {
        public string? TableName { get; set; }
        public string? AccessProperties { get; set; }
        public int RoleId { get; set; }
    }
}
