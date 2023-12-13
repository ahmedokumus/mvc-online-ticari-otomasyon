using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models;

public class ToDoList
{
    public int ToDoListId { get; set; }
    public string ToDoListTitle { get; set; }
    public bool ToDoListState { get; set; }
}