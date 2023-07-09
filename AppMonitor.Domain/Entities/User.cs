using Microsoft.AspNetCore.Identity;

namespace AppMonitor.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<TargetApp> TargetApps { get; set; } 
    }
}