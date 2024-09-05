using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("GetUsers")]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();

        return Ok(users);
    }
    [Authorize]
    [HttpGet("{id:int}")]
  
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var users = await context.Users.FindAsync(id);
        if(users == null) return NotFound();
        return users;
    }

    [HttpPost("SaveUsers")]
    public async Task<ActionResult> SaveUsers([FromBody]AppUser appUser)
    {
       context.Add(appUser);
       int result = await context.SaveChangesAsync();

    // Check if the save was successful
    if (result > 0)
    {
        // Return a success response
        return Ok("Data saved successfully.");
    }
    else
    {
        // Return a failure response
        return StatusCode(StatusCodes.Status500InternalServerError, "Data save failed.");
    }
}
}
