using NewsCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsEntities.Concrete
{
    [Table("Categories")]
    public class Category:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<News> News { get; set; }

    }
}
