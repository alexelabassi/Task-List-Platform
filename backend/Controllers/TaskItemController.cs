using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Task_List_Platform.Dtos.TaskItem.cs;
using Task_List_Platform.Dtos.TaskList;
using Task_List_Platform.Mappers;

namespace Task_List_Platform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskItemController : ControllerBase
{
    private readonly TaskItemRepository _taskItemRepo;

    public TaskItemController(TaskItemRepository taskItemRepo)
    {
        _taskItemRepo = taskItemRepo;
    }

    [HttpGet]
    [Route("/tasks/{taskListId}")]
    public IActionResult GetTasksByList(int taskListId)
    {
        var tasks = _taskItemRepo.GetTasksByList(taskListId);
        if (tasks == null || !tasks.Any())
        {
            return NotFound($"Tasks not found in list with id {taskListId}");
        }

        return Ok(tasks);
    }

    [HttpPost]
    [Route("/task/create/")]
    public IActionResult AddTask([FromBody] CreateTaskItemDto createTaskRequestDto)
    {
        var taskItem = createTaskRequestDto.ToTaskItemFromCreateDto();
        _taskItemRepo.AddTaskItem(taskItem);
        return Ok(taskItem);
    }

    [HttpPut]
    [Route("/task/update/{taskId}")]
    public IActionResult UpdateTask([FromRoute] int taskId, [FromBody] UpdateTaskItemDto updateTaskRequestDto)
    {
        if (string.IsNullOrWhiteSpace(updateTaskRequestDto.Name))
        {
            return BadRequest("Name is required");
        }

        var task = _taskItemRepo.UpdateTaskItem(taskId, updateTaskRequestDto);
        return Ok(task);
    }
        
    [HttpDelete]
    [Route("/task/delete/{taskId}")]
    public IActionResult DeleteTask([FromRoute] int taskId)
    {
        var taskItem = _taskItemRepo.DeleteTaskItem(taskId);
        if (taskItem == null)
        {
            return NotFound();
        }
        return Ok(taskItem);
    }
}