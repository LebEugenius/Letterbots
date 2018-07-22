using System;
using UnityEngine;
using Zenject;

public class AIController : ITickable, IDisposable
{
    public AIController(
        Settings settings,
        PlayerFacade playerTwo,
        IGameMode gameModeController,
        GameController gameController,
        PlayerOneWinSignal playerOneWinSignal,
        PlayerTwoWinSignal playerTwoWinSignal)
    {
        _settings = settings;
        _playerTwo = playerTwo;
        _gameModeController = gameModeController;
        _gameController = gameController;

        _playerOneWinSignal = playerOneWinSignal;
        _playerTwoWinSignal = playerTwoWinSignal;

        _playerOneWinSignal += IncreaseDifficulty;
        _playerTwoWinSignal += Reset;
        Reset();
    }

    private readonly Settings _settings;
    private readonly IGameMode _gameModeController;
    private readonly GameController _gameController;
    private PlayerOneWinSignal _playerOneWinSignal;
    private PlayerTwoWinSignal _playerTwoWinSignal;
    private readonly PlayerFacade _playerTwo;

    private float _currentSpeed;
    private float _timer;

    public void Tick()
    {
        if(_gameController.GameState == GameState.Game) AIAttack();
    }

    private void AIAttack()
    {
        _timer += Time.deltaTime;
        if (_timer < 1f / _currentSpeed) return;

        _playerTwo.Attack();
        _gameModeController.HitPlayerOne(_settings.AIKeyValue);

        _timer = 0;
    }

    public void IncreaseDifficulty()
    {
        _currentSpeed += _settings.AIDifficultyModifier;
    }

    public void Reset()
    {
        _currentSpeed = _settings.StartSpeed;
    }


    public void Dispose()
    {
        _playerOneWinSignal -= IncreaseDifficulty;
        _playerTwoWinSignal -= Reset;
    }

    
    [System.Serializable]
    public class Settings
    {
        public float AIKeyValue;

        public float AIDifficultyModifier;

        public float StartSpeed;
    }
}
