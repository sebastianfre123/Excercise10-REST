namespace Registration_Service;

public class Player
{
    public int PlayerId { get; set; }
    public string Name { get; set; }
    public int EloRating { get; set; }
    public Player()
    {
        
        EloRating = 1500; 
    }
}