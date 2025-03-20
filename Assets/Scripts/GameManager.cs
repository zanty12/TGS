using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

public enum GameState
{
    Build,
    Shoot,
}

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Shoot;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState state)
    {
        gameState = state;
    }
}