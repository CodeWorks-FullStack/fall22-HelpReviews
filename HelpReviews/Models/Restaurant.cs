using System.ComponentModel.DataAnnotations;
using HelpReviews.Interfaces;

namespace HelpReviews.Models;

public class Restaurant : IHasCreator
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  public string ImgUrl { get; set; } = "//thiscatdoesnotexist.com";

  [Required(ErrorMessage = "Please provide a description for your Restaurant....")]
  public string Description { get; set; }
  public int Exposure { get; set; }
  public bool IsShutdown { get; set; } = false;

  public string Tags { get; set; } = "";


  #region Wonky Donkeys Properties

  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public int ReportCount { get; set; } // This will be populated from the count

  #endregion
}