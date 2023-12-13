using System;
using System.ComponentModel.DataAnnotations;

namespace MvcOnlineTicariOtomasyon.Models;

public class CargoDetail
{
    [Key]
    public int CargoDetailId { get; set; }
    public string CargoDetailDescription { get; set; }
    public string CargoDetailTrackingCode { get; set; }
    public string CargoDetailSender { get; set; }
    public string CargoDetailBuyer { get; set; }
    public DateTime Date { get; set; }
}