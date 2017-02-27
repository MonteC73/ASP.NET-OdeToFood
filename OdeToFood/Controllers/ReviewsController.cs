using OdeToFood.Models;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {
        OdeToFoodDb _db = new OdeToFoodDb();

        public ActionResult Index([Bind(Prefix = "id")] int restaurantId)
        {
            var restaurant = _db.Restaurants.Find(restaurantId);
            if (restaurantId != null)
            {
                return View(restaurant);
            }
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
