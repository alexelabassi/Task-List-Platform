using System.ComponentModel.DataAnnotations;

namespace Task_List_Platform.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    // many to one cu TaskList
    public int TaskListId { get; set; }
    public TaskList TaskList { get; set; }
}