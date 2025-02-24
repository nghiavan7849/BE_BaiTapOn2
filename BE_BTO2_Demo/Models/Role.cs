using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BE_BTO2_Demo.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public ICollection<AllowAccess> AllowAccesses { get; set; }

    }
}
