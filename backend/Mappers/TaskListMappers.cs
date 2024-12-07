using Task_List_Platform.Dtos.TaskList;
using Task_List_Platform.Models;

namespace Task_List_Platform.Mappers;

public static class TaskListMappers
{
    public static TaskList ToTaskListFromCreateDto(this CreateTaskListDto createTaskListDto, string userId)
    {
        return new TaskList
        {
            Name = createTaskListDto.Name,
            Description = createTaskListDto.Description,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
        };
    }
}