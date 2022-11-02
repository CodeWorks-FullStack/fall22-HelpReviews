using System.ComponentModel.DataAnnotations;
using HelpReviews.Interfaces;

namespace HelpReviews.Models;

public class Report : IHasCreator
{
  public int Id { get; set; }

  public string Title { get; set; }
  [Range(0, 5)]
  public int Rating { get; set; }
  public string Body { get; set; }
  public int RestaurantId { get; set; }

  #region Wonky Donkeys
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  #endregion

}

public class MyReport : Report
{
  // This class is used when getting all of the reports for a user account
  public Restaurant Restaurant { get; set; }
}