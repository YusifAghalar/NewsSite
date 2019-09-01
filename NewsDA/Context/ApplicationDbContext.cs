using Microsoft.EntityFrameworkCore;
using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsDA.Context
{
   public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext() :base()
        {

        }
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NewsSite;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Category>().HasData(
            new Category() { Id = 1, Name = "World" },
            new Category() { Id = 2, Name = "Sport"  },
            new Category() { Id = 3, Name = "Culture"},
            new Category() { Id = 4, Name = "Life"  },
            new Category() { Id = 5, Name = "Tech"  },
            new Category() { Id = 6, Name = "Fashion"},
            new Category() { Id = 7, Name = "Politics" },
            new Category() { Id = 8, Name = "Business" }
            );

            #region  Initializing news
            List<News> news = new List<News>();
            for (int i = 1; i < 120; i++)
            {
                news.Add
                (new News()
                {
                    Id = i,
                    CategoryId = i % 8 + 1,      //Category with Id=0 doesnt  exits
                    CreatedOn = DateTime.Now - TimeSpan.FromDays(i % 5),
                    Imagepath=null,
                    Text = Faker.Lorem.Paragraph(6),
                    Subtitle=Faker.Lorem.Sentence(7),
                    Title = Faker.Lorem.Sentence(3),
                    IsHeaderNews = i % 8 == 0 ? true : false,
                    
                    
                }
               );
            }

            #endregion

            builder.Entity<News>()
                .HasData(news.ToArray());

            builder.Entity<News>()
                .HasOne(x => x.Category)
                .WithMany(x => x.News)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);

            
        }
    }
}
