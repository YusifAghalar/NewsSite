using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Entities
{
    public class UserWithRole
    {
        public ApplicationUser User { get; set; }
        public string RoleName { get; set; }
    }
}
