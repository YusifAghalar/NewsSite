using Microsoft.AspNetCore.Identity;
using NewsSiteVersion2.Entities;

namespace NewsSiteVersion2.Models
{
    public class DetailsUserModel
    {
         public ApplicationUser User { get; set; }
         public string RoleName { get; set; }

    }
    
}