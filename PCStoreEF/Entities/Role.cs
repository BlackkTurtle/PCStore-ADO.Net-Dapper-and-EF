using Microsoft.AspNetCore.Identity;

namespace PCStoreEF.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role(string roleName) : base(roleName)
        {
        }

        public string RoleName { get; set; } = null!;
    }
}
