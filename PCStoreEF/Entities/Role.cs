using Microsoft.AspNetCore.Identity;

namespace PCStoreEF.Entities
{
    public class Role : IdentityRole<int>
    {
        public string RoleName { get; set; } = null!;
    }
}
