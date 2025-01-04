
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController(IMediator mediator) : BaseApiController(mediator)
{
    
    /// <summary>
    /// Retrieves all activities from the database.
    /// </summary>
    /// <returns>A list of all activities in the database.</returns>
    [HttpGet]
    public async Task<ActionResult> GetActivities()
    {
        return HandleResult(await MediatorObj.Send(new ActivityList.Query()));
    }

    /// <summary>
    /// Retrieves an activity by its identifier from the database.
    /// </summary>
    /// <param name="id">The identifier of the activity to retrieve.</param>
    /// <returns>The activity with the specified <paramref name="id"/>,
    /// or a 404 error if no such activity exists.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetActivity(Guid id)
    {
        return HandleResult(await MediatorObj.Send(new ActivityDetail.Query { Id = id }));
    }

    /// <summary>
    /// Creates a new activity in the database.
    /// </summary>
    /// <param name="activity">The activity to add to the database.</param>
    /// <returns>A 200 OK response if the activity was successfully added.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] Activity activity)
    {
        return HandleResult(await MediatorObj.Send(new CreateActivity.Command { Activity = activity }));
    }

    /// <summary>
    ///  edit activity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="activity"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        return HandleResult(await MediatorObj.Send(new EditActivity.Command { Activity = activity }));
    }

    /// <summary>
    /// Deletes the activity with the specified <paramref name="id"/> from the database.
    /// </summary>
    /// <param name="id">The identifier of the activity to delete.</param>
    /// <returns>A 200 OK response if the activity was successfully deleted.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        return HandleResult(await MediatorObj.Send(new DeleteActivity.Command { Id = id }));
    }
}