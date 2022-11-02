namespace HelpReviews.Repositories;

public class ReportsRepo : BaseRepo
{

  public List<Report> GetReportsByRestaurantId(int restaurantId)
  {
    var sql = @"
    SELECT 
      report.*,
      a.*
    FROM reports report
    JOIN accounts a ON a.id = report.creatorId
    WHERE report.restaurantId = @restaurantId
    ;";

    return _db.Query<Report, Profile, Report>(sql, (r, p) =>
    {
      r.Creator = p;
      return r;
    }, new { restaurantId }).ToList();
  }

  public List<MyReport> GetMyReports(string creatorId)
  {
    var sql = @"
      SELECT 
        report.*,
        a.*,
        restaurant.*
      FROM reports report
      JOIN accounts a ON a.id = report.creatorId
      JOIN restaurants restaurant ON restaurant.id = report.restaurantId
      WHERE report.creatorId = @creatorId
    ;";

    return _db.Query<MyReport, Profile, Restaurant, MyReport>(sql, (mr, profile, restaurant) =>
    {
      mr.Restaurant = restaurant;
      mr.Creator = profile;
      return mr;
    }, new { creatorId }).ToList();
  }


  public Report GetReportById(int id)
  {

    var sql = @"
    SELECT 
      report.*,
      a.*
    FROM reports report
    JOIN accounts a ON a.id = report.creatorId
    WHERE report.id = @id
    ;";

    return _db.Query<Report, Profile, Report>(sql, (r, p) =>
    {
      r.Creator = p;
      return r;
    }, new { id }).FirstOrDefault();
  }

  public Report CreateReport(Report data)
  {
    var sql = @"
      INSERT INTO reports(
        title,
        body,
        rating,
        restaurantId,
        creatorId
      )
      VALUES(
        @Title,
        @Body,
        @Rating,
        @RestaurantId,
        @CreatorId
      );
      SELECT LAST_INSERT_ID()
    ;";

    int id = _db.ExecuteScalar<int>(sql, data);
    return GetReportById(id);
  }


  public bool DeleteReport(int id)
  {
    return _db.Execute("DELETE FROM reports WHERE id = @id", new { id }) == 1;
  }


  public ReportsRepo(IDbConnection db) : base(db)
  {
  }
}

