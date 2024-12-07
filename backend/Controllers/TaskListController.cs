using DefaultNamespace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task_List_Platform.Dtos.TaskList;
using Task_List_Platform.Mappers;
using Task_List_Platform.Models;

namespace Task_List_Platform.Controllers;

[Route("api/tasklist")]
[ApiController]
public class TaskListController : ControllerBase
{
    private readonly TaskListRepository _taskListRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    public TaskListController(TaskListRepository taskListRepo, UserManager<ApplicationUser> userManager)
    {
        _taskListRepo = taskListRepo;
        _userManager = userManager;
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
    [Authorize]
    public IActionResult CreateTaskList([FromBody] CreateTaskListDto createTaskListDto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }
        var taskList = createTaskListDto.ToTaskListFromCreateDto(userId);
        _taskListRepo.Create(taskList);
        return Ok(taskList);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    [Authorize]
    public IActionResult DeleteTaskList([FromRoute] int id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }
        var taskList = _taskListRepo.GetTaskListById(id);
        if (taskList.UserId != userId)
        {
            return Unauthorized();
        }
        _taskListRepo.Delete(id);
        if (taskList == null)
        {
            return NotFound();
        }
        return Ok(taskList);
    }
}