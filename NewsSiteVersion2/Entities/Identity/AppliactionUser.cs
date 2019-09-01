using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public CustomRole UserRole { get; set; }
        public string CustomRoleId { get; set; }
    }

    
}
