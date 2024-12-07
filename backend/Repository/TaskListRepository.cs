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

    public IEnumerable<TaskList> GetTaskListsForUser(int userId)
    {
        return _context.TaskLists.Where(t => t.UserId == userId);
    }

    public TaskList? UpdateTaskList(int id, UpdateListRequestDto updateListRequestDto)
    {
        var taskList = _context.TaskLists.FirstOrDefault(t => t.Id == id);
        if (taskList == null)
        {
            return null;
        }
        taskList.Name = updateListRequestDto.Name;
        _context.SaveChanges();
        return taskList;
    }
}