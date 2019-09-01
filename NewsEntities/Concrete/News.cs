using NewsCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsEntities.Concrete
{
    [Table("News")]
    public class News : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool IsHeaderNews { get; set; }

        public string Imagepath { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string AuthorId { get; set; }

        public Category Category { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }

}

