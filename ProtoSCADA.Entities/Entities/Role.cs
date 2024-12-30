using System.ComponentModel.DataAnnotations;

namespace ProtoSCADA.Entities.Entities
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        public string RoleName { get; set; } = string.Empty;
    }
}
