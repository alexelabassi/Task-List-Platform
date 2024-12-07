using Task_List_Platform.Dtos.TaskItem.cs;
using Task_List_Platform.Models;

namespace Task_List_Platform.Mappers;

public static class TaskItemMappers
{
    public static TaskItem ToTaskItemFromCreateDto(this CreateTaskItemDto taskDto)
    {
        return new TaskItem()
        {
            Name = taskDto.Name,
            TaskListId = taskDto.TaskListId
        };
    }
}