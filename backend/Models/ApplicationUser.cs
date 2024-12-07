using Microsoft.AspNetCore.Identity;

namespace Task_List_Platform.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();
}