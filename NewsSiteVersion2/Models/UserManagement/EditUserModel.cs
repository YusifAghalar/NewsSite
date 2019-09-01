using Microsoft.AspNetCore.Mvc.Rendering;
using NewsSiteVersion2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Models.UserManagement
{
    public class EditUserModel
    {
        public SelectList Roles { get; set; }
        public ApplicationUser User  { get; set; }
        public CustomRole Role { get; set; }
    }
}
