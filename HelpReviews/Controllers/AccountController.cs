namespace HelpReviews.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly ReportsService _reportsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, ReportsService reportsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _reportsService = reportsService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("__my-restaurant-reports__💓_BigJerms")]
  public async Task<ActionResult<List<MyReport>>> GetMyReports()
  {
    try
    {
      var userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      var myReports = _reportsService.GetMyReports(userInfo?.Id);

      return Ok(myReports);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
