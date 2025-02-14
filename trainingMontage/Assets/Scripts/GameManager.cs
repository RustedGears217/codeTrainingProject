
using System;
using System.Collections;
using UnityEngine;


//An enum of all the possible GameStates (Many are Gameplay Modes!)
[Serializable]
public enum GameState
{
    Starting = 1,
    Playing = 10,
    Paused = 15,
    FailScreen = 20,
    VictoryDance = 25
}

public class GameManager : Singleton<GameManager>
{
    public GameState State { get; private set; }
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    private GameState _previousState = GameState.Starting;

    void Start()
    {
        //Begin with the "Starting" game state
        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        Debug.Log("Changed Game State to    : " + newState);

        _previousState = State;

        if (_previousState == GameState.Paused)
        {
            Time.timeScale = 1;
        }

        //This Game Manager can do high level manager stuff, itself.
        switch (newState)
        {
            case GameState.Starting:
                StartCoroutine(HandleStarting());
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.FailScreen:
                break;
            case GameState.VictoryDance:
                break;
            default:
                Debug.Log("GameState not handled: " + nameof(newState));
                //throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
                break;
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    private IEnumerator HandleStarting()
    {
        //Play music here?
        yield return new WaitForSeconds(2);
        ChangeState(GameState.Playing);
    }

    public void TogglePause()
    {
        if (State == GameState.Paused)
        {
            ChangeState(_previousState);
        }
        else
        {
            ChangeState(GameState.Paused);
        }
    }
}


