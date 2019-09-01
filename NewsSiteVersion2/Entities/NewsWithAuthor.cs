using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Entities
{
    public class NewsWithAuthor
    {
        public News News { get; set; }
        public string AuthorName { get; set; }
    }
}
