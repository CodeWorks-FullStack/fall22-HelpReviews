namespace HelpReviews.Services;

public class RestaurantsService
{
  private readonly RestaurantsRepository _rr;
  private readonly ReportsRepo _reportsRepo;


  public List<Restaurant> GetRestaurants()
  {
    return _rr.GetRestaurants();
  }

  public Restaurant GetRestaurant(int id)
  {
    var restaurant = _rr.GetRestaurant(id);
    if (restaurant == null)
    {
      throw new Exception("nah thats a bad Restaurant Id");
    }
    restaurant.Exposure++;
    UpdateRestaurant(restaurant); // just do the thing....

    return restaurant;
  }


  public void UpdateRestaurant(Restaurant r)
  {
    _rr.UpdateRestaurant(r);
  }


  public Restaurant UpdateRestaurant(Restaurant restaurant, string userId)
  {
    var original = GetRestaurant(restaurant.Id);
    if (original.CreatorId != userId)
    {
      throw new Exception("no no no.... ☝️ that is not yours");
    }

    original.Name = restaurant.Name;
    original.IsShutdown = restaurant.IsShutdown;
    original.Description = restaurant.Description;
    original.ImgUrl = restaurant.ImgUrl == "//thiscatdoesnotexist.com" ? original.ImgUrl : restaurant.ImgUrl;
    original.Exposure = restaurant.Exposure;

    var updated = _rr.UpdateRestaurant(original);
    return updated;
  }


  public void DeleteRestaurant(int id, string userId)
  {
    var restaurant = GetRestaurant(id);
    if (restaurant.CreatorId != userId)
    {
      throw new Exception("No not yours....");
    }

    _rr.Delete(id);
  }

  internal Restaurant CreateRestaurant(Restaurant data)
  {
    return _rr.CreateRestaurant(data);
  }

  public RestaurantsService(RestaurantsRepository rr, ReportsRepo reportsRepo)
  {
    _rr = rr;
    _reportsRepo = reportsRepo;
  }

  internal Restaurant ShutdownRestaurant(int id)
  {
    var restaurant = GetRestaurant(id);
    var reports = _reportsRepo.GetReportsByRestaurantId(id);
    if (reports.Count >= 3 && reports.Sum(r => r.Rating) < reports.Count)
    {
      restaurant.IsShutdown = true;
      UpdateRestaurant(restaurant);
    }
    return restaurant;
  }
}
