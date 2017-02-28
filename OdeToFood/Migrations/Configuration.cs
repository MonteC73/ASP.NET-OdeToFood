using OdeToFood.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;

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

            for (int i = 0; i < 1000; i++)
            {
                context.Restaurants.AddOrUpdate(r => r.Name,
                    new Restaurant { Name = "Restaurant" + i, City = "Nowhere" + (i%30 + 1), Country = "USA" } );
            }

            SeedMenbership();

        }

        private void SeedMenbership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserId", "UserName", autoCreateTables:true);

            var roles = (SimpleRoleProvider) Roles.Provider;
            var membership = (SimpleMembershipProvider) Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("sallen", false) == null)
            {
                membership.CreateUserAndAccount("sallen", "imalittleteapot");
            }
            if (!roles.GetRolesForUser("sallen").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] {"sallen"}, new[] {"Admin"});
            }
        }
    }
}
