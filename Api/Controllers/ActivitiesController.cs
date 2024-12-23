using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController(DataContext context):ControllerBase
{
    private readonly DataContext _context = context;
    
    /// <summary>
    /// Retrieves all activities from the database.
    /// </summary>
    /// <returns>A list of all activities in the database.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await _context.Activities.ToListAsync();
    }
    
    /// <summary>
    /// Retrieves an activity by its identifier from the database.
    /// </summary>
    /// <param name="id">The identifier of the activity to retrieve.</param>
    /// <returns>The activity with the specified <paramref name="id"/>,
    /// or a 404 error if no such activity exists.</returns>

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
   {
        return await _context.Activities.FindAsync(id);
    }
  
}