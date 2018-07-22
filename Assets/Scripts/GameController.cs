using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public enum GameState
{
    Idle,
    Prepare,
    Game,
    Lose,
    Win
}

public class GameController : ITickable, IInitializable
{
    public GameController(
        Settings settings, 
        PrepareSignal prepareSignal, 
        StartSignal startSignal,
        PlayerOneRecoverSignal playerOneRecoverSignal,
        PlayerTwoRecoverSignal playerTwoRecoverSignal)
    {
        _settings = settings;
        _prepareSignal = prepareSignal;
        _startSignal = startSignal;

        _playerOneRecoverSignal = playerOneRecoverSignal;
        _playerTwoRecoverSignal = playerTwoRecoverSignal;      
    }

    public GameState GameState { get; private set; }

    private readonly Settings _settings;
    private readonly PrepareSignal _prepareSignal;
    private readonly StartSignal _startSignal;

    private readonly PlayerOneRecoverSignal _playerOneRecoverSignal;
    private readonly PlayerTwoRecoverSignal _playerTwoRecoverSignal;

    [DllImport("__Internal")]
    private static extern void SaveRecord();


    private float _gameStartTime;
    public int CurrentLevel = 1;
    public int RecordLevel = 1;
    
    public void Initialize()
    {
        GameState = GameState.Idle;
        RecordLevel = PlayerPrefs.GetInt("Record");
    }
    public void Tick()
    {
        switch (GameState)
        {
            case GameState.Idle:
                IdleUpdate();
                break;
            case GameState.Prepare:
                PrepareUpdate();
                break;
            case GameState.Lose:
            case GameState.Win:
                FinishedUpdate();
                break;
        }
    }

    private void PrepareUpdate()
    {
        if (GetTimeBeforeStart() >= 0) return;

        _startSignal.Fire();
        GameState = GameState.Game;
    }
    private void IdleUpdate()
    {
        if (Input.anyKeyDown)
        {
            GameState = GameState.Prepare;
            _prepareSignal.Fire();
            _gameStartTime = Time.timeSinceLevelLoad;
            _playerOneRecoverSignal.Fire();
            _playerTwoRecoverSignal.Fire();
        }
    }

    private void FinishedUpdate()
    {
        if (GetNewRoundTime() <= 0)
            GameState = GameState.Idle;
    }

    public void OnPlayerOneWin()
    {
        GameState = GameState.Win;
        _gameStartTime = Time.timeSinceLevelLoad;
        RecordLevel = Math.Max(RecordLevel, CurrentLevel++);
        PlayerPrefs.SetInt("Record", RecordLevel);
        //SaveRecord();
        Application.ExternalCall("kongregate.stats.submit", "Record", RecordLevel);
    }
    public void OnPlayerTwoWin()
    {
        GameState = GameState.Lose;
        _gameStartTime = Time.timeSinceLevelLoad;
        CurrentLevel = 1;
    }
    
    public float GetTimeBeforeStart()
    {
        return _settings.PrepareTime + _gameStartTime - Time.timeSinceLevelLoad;
    }

    public float GetNewRoundTime()
    {
        return _settings.NewRoundDelay + _gameStartTime - Time.timeSinceLevelLoad;
    }

    [System.Serializable]
    public class Settings
    {
        public float PrepareTime;
        public float NewRoundDelay;
    }
}
