using Org.BouncyCastle.Bcpg;

namespace Registration_Service;

// Controllers/ValuesController.cs

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ValuesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Player player)
    {
        try
        {
            if (player == null)
            {
                return BadRequest("Invalid data");
            }

            _dbContext.Players.Add(player);
            await _dbContext.SaveChangesAsync();

            return Ok("Player registered successfully");
        }
        catch (Exception ex)
        {
            // Log the exception details
            Console.WriteLine($"Exception: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating new player record. Exception: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] PlayerUpdateDTO playerUpdate)
    {
        try
        {
            var existingPlayer = await _dbContext.Players.FindAsync(id);

            if (existingPlayer == null)
            {
                return NotFound($"Player with ID {id} not found");
            }

            // Update only if the property is provided in the DTO
            if (playerUpdate.Name != null)
            {
                existingPlayer.Name = playerUpdate.Name;
            }

            if (playerUpdate.EloRating != 0) // Assuming 0 is not a valid EloRating
            {
                existingPlayer.EloRating = playerUpdate.EloRating;
            }

            // Update other properties similarly if needed

            _dbContext.Players.Update(existingPlayer);
            await _dbContext.SaveChangesAsync();

            return Ok($"Player with ID {id} updated successfully");
        }
        catch (Exception ex)
        {
            // Handle exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating player record. Exception: {ex.Message}");
        }
    }
    
    [HttpGet]
    public IActionResult GetAllPlayers()
    {
        try
        {
            var players = _dbContext.Players.ToList();
            return Ok(players);
        }
        catch (Exception ex)
        {
            // Log and handle exceptions
            Console.WriteLine($"Exception: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving players. Exception: {ex.Message}");
        }
    }

}
public class PlayerUpdateDTO
{
    public string? Name { get; set; }
    public int EloRating { get; set; }
    // Add other properties if needed
}