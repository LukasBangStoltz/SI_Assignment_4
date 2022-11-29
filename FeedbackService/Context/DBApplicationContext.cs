using FeedbackService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FeedbackService.Context
{
    public class DBApplicationContext : DbContext
    {
        public DBApplicationContext(DbContextOptions<DBApplicationContext> options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            Seed(builder);
            base.OnModelCreating(builder);
        }
        private void Seed(ModelBuilder builder)
        {
            builder.Entity<Review>().HasData(
                new Review { UserId = 1, DeliveryId = 1, RestaurantId = 1, Id = 1, Message = "Det var godt nok noget dårlig mad mand!!!", Rating = 1, ReviewDate = DateTime.Now },
                new Review { UserId = 2, DeliveryId = 1, RestaurantId = 1, Id = 2, Message = "Det var godt nok noget lækker mad mand!!!", Rating = 5, ReviewDate = DateTime.Now }



                );
        }

    }
}
