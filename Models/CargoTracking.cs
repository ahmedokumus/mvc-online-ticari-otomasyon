using System;
using System.ComponentModel.DataAnnotations;

namespace MvcOnlineTicariOtomasyon.Models;

public class CargoTracking
{
    [Key]
    public int CargoTrackingId { get; set; }
    public string CargoTrackingCargoTracking { get; set; }
    public string CargoTrackingDescription { get; set; }
    public DateTime Date { get; set; }
}