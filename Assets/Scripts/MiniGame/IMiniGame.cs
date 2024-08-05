using System;

public interface IMiniGame
{
    public void StartGame();

    public void EndGame();

    public Action OnGameStarted { get; set; }
    public Action OnGameEnded { get; set; }
}