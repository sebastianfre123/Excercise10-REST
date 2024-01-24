using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;

namespace MatchmakingService.Controllers;


[ApiController]
[Route("matchmaking")]
public class MatchmakingController : ControllerBase
{
    private ILogger<MatchmakingController> _logger;
    public List<PlayerData> PData = new List<PlayerData>();
    public List<PlayerStatistic> PStatistic = new List<PlayerStatistic>();

    public MatchmakingController(ILogger<MatchmakingController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet("GetPlayer")]
    public string GetPlayers([FromBody] List<PlayerData> playerData)
    {
        foreach (var data in playerData)
        {
            _logger.LogInformation($"{data.Id} - {data.Name}");
        }

        PData = playerData;
        
        return $"Hallo {playerData}";
    }

    [HttpPost("GetStatistic")]
    public string GetStatistic([FromBody] List<PlayerStatistic> playerStatistics)
    {
        foreach (var data in playerStatistics)
        {
            _logger.LogInformation($"{data.Id} - {data.EloRating} - {data.LastDuelPlayedAt}");
        }

        PStatistic = playerStatistics;
        
        return $"Hallo {playerStatistics}";
    }

    [HttpPost("Matchmaking")]
    public void MatchPlayer()
    {

        /*if (PData == null || PStatistic == null)
        {
            _logger.LogError("Die Listen PData und PStatistic müssen initialisiert sein.");
            //return BadRequest("Die Listen PData und PStatistic müssen initialisiert sein.");
        }*/

        var matchedPlayers = new List<MatchedPlayer>();

        foreach (var data in PData)
        {
            var stat = PStatistic.FirstOrDefault(s => s.Id == data.Id);

            if (stat != null)
            {
                matchedPlayers.Add(new MatchedPlayer
                {
                    PlayerId = data.Id,
                    PlayerName = data.Name,
                    Rating = stat.EloRating,
                    LastDuelPlayed = stat.LastDuelPlayedAt
                });
            }
        }

        foreach (var matchedPlayer in matchedPlayers)
        {
            _logger.LogInformation($"{matchedPlayer.PlayerId} - {matchedPlayer.PlayerName} - {matchedPlayer.Rating} - {matchedPlayer.LastDuelPlayed}");
        }


        //return matchedPlayers;
        
        _logger.LogInformation("Pepsi");
    }
    
}

public class PlayerData
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PlayerStatistic    
{
    public int Id { get; set; }
    public int EloRating { get; set; }
    public DateTime LastDuelPlayedAt { get; set; }
}

public class MatchedPlayer
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public int Rating { get; set; }
    public DateTime LastDuelPlayed { get; set; }
}