namespace HelpReviews.Repositories;

public class RestaurantsRepository : BaseRepo
{

  public List<Restaurant> GetRestaurants()
  {
    var sql = @"
      SELECT 
        r.*,
        COUNT(rep.id) AS ReportCount,
        a.* 
      FROM restaurants r
      JOIN accounts a ON a.id = r.creatorId
      LEFT JOIN reports rep ON rep.restaurantId = r.id
      WHERE r.isShutdown = false
        GROUP BY r.id
    ;";

    return _db.Query<Restaurant, Profile, Restaurant>(sql, (r, p) =>
    {
      r.Creator = p;
      return r;
    }).ToList();
  }

  public Restaurant GetRestaurant(int id)
  {
    var sql = @"
      SELECT 
        r.*,
        COUNT(rep.id) AS ReportCount,
        a.* 
      FROM restaurants r
      JOIN accounts a ON a.id = r.creatorId
      LEFT JOIN reports rep ON rep.restaurantId = r.id
      WHERE r.id =  @id 
        GROUP BY r.id
      ;";

    return _db.Query<Restaurant, Profile, Restaurant>(sql, (r, p) =>
    {
      r.Creator = p;
      return r;
    }, new { id }).FirstOrDefault();
  }


  public Restaurant CreateRestaurant(Restaurant data)
  {
    var sql = @"
      INSERT INTO restaurants(
        name,
        imgUrl, 
        description, 
        exposure,
        tags,
        creatorId
      )
      VALUES(
        @Name,
        @ImgUrl,
        @Description,
        @Exposure,
        @Tags,
        @CreatorId
      );
      SELECT LAST_INSERT_ID()
    ;";
    data.CreatedAt = DateTime.Now;
    data.UpdatedAt = DateTime.Now;
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return data;
  }


  public Restaurant UpdateRestaurant(Restaurant data)
  {
    var sql = @"
      UPDATE restaurants SET
        name = @Name,
        imgUrl = @ImgUrl,
        description = @Description,
        exposure = @Exposure,
        isShutdown = @IsShutdown
      WHERE id = @Id
    ;";
    data.UpdatedAt = DateTime.Now;
    _db.Execute(sql, data);

    return data;
  }

  public void Delete(int id)
  {
    _db.Execute("DELETE FROM restaurants WHERE id = @id", new { id });
  }


  public RestaurantsRepository(IDbConnection db) : base(db)
  {
  }
}

