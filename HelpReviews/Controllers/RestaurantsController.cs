namespace HelpReviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{

  private readonly RestaurantsService _rs;
  private readonly ReportsService _reportsService;
  private readonly Auth0Provider _ape;

  public RestaurantsController(RestaurantsService rs, ReportsService reportsService, Auth0Provider ape)
  {
    _rs = rs;
    _reportsService = reportsService;
    _ape = ape;
  }

  [HttpGet]
  public ActionResult<List<Restaurant>> GetRestaurants()
  {
    try
    {
      var restaurants = _rs.GetRestaurants();
      return Ok(restaurants);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Restaurant> GetRestaurant(int id)
  {
    try
    {
      var restaurant = _rs.GetRestaurant(id);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/reports")]
  public ActionResult<List<Report>> GetRestaurantReports(int id)
  {
    try
    {
      var reports = _reportsService.GetReportsByRestaurantId(id);
      return Ok(reports);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] Restaurant data)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("You are a bad user..... or your token is crappy... 🤷‍♂️");
      }
      data.CreatorId = userInfo?.Id;
      var restaurant = _rs.CreateRestaurant(data);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost("{id}/reports")]
  public async Task<ActionResult<Report>> CreateRestaurantReport([FromBody] Report data, int id)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("You are a bad user..... or your token is crappy... 🤷‍♂️");
      }
      data.CreatorId = userInfo?.Id;
      data.RestaurantId = id;
      var report = _reportsService.CreateReport(data);

      return Ok(report);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<Restaurant>> UpdateRestaurant([FromBody] Restaurant data, int id)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      data.Id = id;
      var restaurant = _rs.UpdateRestaurant(data, userInfo?.Id);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{id}/shut-it-down")]
  public async Task<ActionResult<Restaurant>> Shutdown(int id)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      var restaurant = _rs.ShutdownRestaurant(id);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



  [Authorize]
  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteRestaurant(int id)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      _rs.DeleteRestaurant(id, userInfo?.Id);
      return Ok("Restaurant Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }




}