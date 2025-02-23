﻿namespace BE_BTO2_Demo.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? HomeAddress { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
