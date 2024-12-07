using Task_List_Platform.Data;
using Task_List_Platform.Dtos.TaskList;
using Task_List_Platform.Models;

namespace DefaultNamespace;

public class TaskListRepository
{
    private readonly ApplicationDbContext _context;

    public TaskListRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public TaskList? GetTaskListById(int id)
    {
        return _context.TaskLists.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<TaskList> GetTaskListsForUser(string userId)
    {
        return _context.TaskLists.Where(t => t.UserId == userId);
    }

    public TaskList? UpdateTaskList(int id, UpdateTaskListDto updateTaskListDto)
    {
        var taskList = _context.TaskLists.FirstOrDefault(t => t.Id == id);
        if (taskList == null)
        {
            return null;
        }
        taskList.Name = updateTaskListDto.Name;
        _context.SaveChanges();
        return taskList;
    }

    public TaskList Create(TaskList taskList)
    {
        _context.TaskLists.Add(taskList);
        _context.SaveChanges();
        return taskList;
    }

    public TaskList? Delete(int id)
    {
        var taskList = _context.TaskLists.FirstOrDefault(t => t.Id == id);
        if (taskList == null)
        {
            return taskList;
        }
        _context.TaskLists.Remove(taskList);
        _context.SaveChanges();
        return taskList;
    }
}