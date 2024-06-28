using TMPro;

public class GameHUDController : IUIController
{
    public GameHUDScoreCounter scoreText;  
    public GameHUDMoveCounter movesText;

    public int score;
    public int moves;

    public GameHUDController(GameHUDScoreCounter scoreText, GameHUDMoveCounter movesText)
    {
        this.scoreText = scoreText;
        this.movesText = movesText;

        scoreText.SetupController(this);
        movesText.SetupController(this);
    }

    public void Activate()
    {
        scoreText.gameObject.SetActive(true);
        movesText.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        scoreText.gameObject.SetActive(false);
        movesText.gameObject.SetActive(false);
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        scoreText.UpdateScoreText(); 
    }

    public void IncrementMoves()
    {
        moves++;
        movesText.UpdateMovesText(); 
    }


}
