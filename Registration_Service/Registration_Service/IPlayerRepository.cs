namespace Registration_Service;

public interface IPlayerRepository
{
    Task<Player> AddPlayer(Player player);
    Task<Player> GetPlayerById(int playerId);
}
public class PlayerRepository : IPlayerRepository
{
    private readonly AppDbContext _dbContext;

    public PlayerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Player> AddPlayer(Player player)
    {
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync();
        return player;
    }

    public async Task<Player> GetPlayerById(int playerId)
    {
        return await _dbContext.Players.FindAsync(playerId);
    }
    // Implement additional methods as needed
}