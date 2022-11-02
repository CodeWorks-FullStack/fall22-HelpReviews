namespace HelpReviews.Services;

public class ReportsService
{
  private readonly ReportsRepo _reportsRepo;


  public List<Report> GetReportsByRestaurantId(int restaurantId)
  {
    return _reportsRepo.GetReportsByRestaurantId(restaurantId);
  }


  public List<MyReport> GetMyReports(string creatorId)
  {
    return _reportsRepo.GetMyReports(creatorId);
  }

  public Report CreateReport(Report data)
  {
    return _reportsRepo.CreateReport(data);
  }

  public Report GetReport(int id)
  {
    var report = _reportsRepo.GetReportById(id);
    if (report == null)
    {
      throw new Exception("Bad Report Id");
    }
    return report;
  }


  public string DeleteReport(int reportId, string userId)
  {

    var report = GetReport(reportId);
    if (report.CreatorId != userId)
    {
      throw new Exception("nah 🐕 you get out of here this report isn't yours...");
    }

    var deleted = _reportsRepo.DeleteReport(reportId);
    if (!deleted)
    {
      throw new Exception("something broke.... not sure what it was");
    }
    return "Successfully deleted your report";
  }




  public ReportsService(ReportsRepo reportsRepo)
  {
    _reportsRepo = reportsRepo;
  }
}