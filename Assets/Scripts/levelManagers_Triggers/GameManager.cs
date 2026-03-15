using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public enum gameState
    {
        playing,
        paused,
        dead
    }

    gameState _currentGameState = gameState.playing;

    public void SetGameState(gameState gameStateToSet)
    {
        _currentGameState = gameStateToSet;
    }
    public bool GameIsPaused()
    {
        return _currentGameState == gameState.paused;
    }
    public bool GameIsPlaying()
    {
        return _currentGameState == gameState.playing;
    }
}
