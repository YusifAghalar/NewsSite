using NewsSiteVersion2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace NewsSiteVersion2.Models
{
    public class IndexUserModel
    {
        public List<UserWithRole> Users { get; set; }
        public IPagedList<ApplicationUser> tempUser { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
    }
}
