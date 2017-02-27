using OdeToFood.Models;
using System.Collections.Generic;

namespace OdeToFood.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OdeToFood.Models.OdeToFoodDb";
        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
            context.Restaurants.AddOrUpdate(p => p.Name,
                new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA"},
                new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA"},
                new Restaurant 
                { 
                    Name = "Smaka", City = "Gothenburg", Country = "Sweden", 
                    Reviews = 
                    new List<RestaurantReview>
                    {
                        new RestaurantReview { Rating = 9, Body="Great food!", ReviewerName = "Scott"}
                    }
                });
        }
    }
}
