using Task_List_Platform.Data;
using Task_List_Platform.Dtos.TaskItem.cs;
using Task_List_Platform.Models;

namespace DefaultNamespace;

public class TaskItemRepository
{
    private readonly ApplicationDbContext _context;

    public TaskItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TaskItem> GetTasksByList(int taskListId)
    {
        var tasks = _context.TaskItems.Where(t => t.TaskListId == taskListId);
        return tasks;
    }

    public TaskItem AddTaskItem(TaskItem taskItem)
    {
        _context.TaskItems.Add(taskItem);
        _context.SaveChanges();
        return taskItem;
    }

    public TaskItem? DeleteTaskItem(int taskItemId)
    {
        var taskItem = _context.TaskItems.FirstOrDefault(t => t.Id == taskItemId);
        if (taskItem == null)
        {
            return taskItem;
        }

        _context.TaskItems.Remove(taskItem);
        _context.SaveChanges();
        return taskItem;
    }

    public TaskItem? UpdateTaskItem(int taskItemId, UpdateTaskItemDto updateTaskRequestDto)
    {
        var taskItem = _context.TaskItems.FirstOrDefault(t => t.Id == taskItemId);
        if (taskItem == null)
        {
            return taskItem;
        }

        if (updateTaskRequestDto.Name != "")
        {
            taskItem.Name = updateTaskRequestDto.Name;
        }

        _context.SaveChanges();
        return taskItem;
    }
}