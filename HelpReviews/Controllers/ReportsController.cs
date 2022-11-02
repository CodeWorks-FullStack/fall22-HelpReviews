namespace HelpReviews.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{

  private readonly Auth0Provider _ape;
  private readonly ReportsService _repService;

  public ReportsController(Auth0Provider ape, ReportsService repService)
  {
    _ape = ape;
    _repService = repService;
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteReport(int id)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      _repService.DeleteReport(id, userInfo?.Id);
      return Ok("Delorted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}