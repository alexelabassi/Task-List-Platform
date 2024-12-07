using System.ComponentModel.DataAnnotations;

namespace Task_List_Platform.Models;

public class TaskList
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // many to one cu User
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    // one to many cu Task
    // public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}