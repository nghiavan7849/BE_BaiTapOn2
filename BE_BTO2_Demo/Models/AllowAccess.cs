﻿namespace BE_BTO2_Demo.Models
{
    public class AllowAccess
    {
        public int Id { get; set; }
        public string? TableName { get; set; }
        public string? AccessProperties { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
