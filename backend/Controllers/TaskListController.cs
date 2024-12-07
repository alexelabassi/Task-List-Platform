using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Task_List_Platform.Dtos.TaskList;
using Task_List_Platform.Mappers;

namespace Task_List_Platform.Controllers;

[Route("api/tasklist")]
[ApiController]
public class TaskListController : ControllerBase
{
    private readonly TaskListRepository _taskListRepo;

    public TaskListController(TaskListRepository taskListRepo)
    {
        _taskListRepo = taskListRepo;
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var taskList = _taskListRepo.GetTaskListById(id);
        if (taskList == null)
        {
            return NotFound();
        }

        return Ok(taskList);
    }

    [HttpGet]
    [Route("showAllForUser/{userId}")]
    public IActionResult GetAllForUser([FromRoute] string userId)
    {
        var taskLists = _taskListRepo.GetTaskListsForUser(userId);
        if (taskLists == null || !taskLists.Any())
        {
            return NotFound($"The user with id {userId} has no task lists");
        }

        return Ok(taskLists);
    }

    [HttpPut]
    [Route("edit/{listId}")]
    public IActionResult UpdateTaskList([FromRoute] int listId, [FromBody] UpdateTaskListDto updateListRequestDto)
    {
        if (string.IsNullOrWhiteSpace(updateListRequestDto.Name))
        {
            return BadRequest(new { Message = "No name provided" });
        }

        var taskList = _taskListRepo.UpdateTaskList(listId, updateListRequestDto);
        return Ok(taskList);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateTaskList([FromBody] CreateTaskListDto createTaskListDto)
    {
        var taskList = createTaskListDto.ToTaskListFromCreateDto();
        _taskListRepo.Create(taskList);
        return Ok(taskList);
    }
}