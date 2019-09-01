using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Entities
{
    public class CustomRole:IdentityRole
    {
        public  virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
