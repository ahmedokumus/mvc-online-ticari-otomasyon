using System;
using System.ComponentModel.DataAnnotations;

namespace MvcOnlineTicariOtomasyon.Models;

public class Message : ModelsBase
{
    [Key]
    public int MessageId { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

}