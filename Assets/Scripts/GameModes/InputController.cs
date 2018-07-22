using System;
using UnityEngine;
using Zenject;

public class InputController : ITickable
{
    public InputController(
        Settings settings,
        PlayerFacade playerOne,
        IGameMode gameModeController,
        GameController gameController,
        IncorrectKeyPressedSignal incorrectKey)
    {
        _settings = settings;
        _playerOne = playerOne;
        _gameModeController = gameModeController;
        _gameController = gameController;
        _incorrectKeyPressedSignal = incorrectKey;
        ChangeCurrentKeyCode();
    }

    private readonly Settings _settings;

    private readonly IGameMode _gameModeController;
    private readonly GameController _gameController;
    private readonly PlayerFacade _playerOne;

    private IncorrectKeyPressedSignal _incorrectKeyPressedSignal;

    public KeyCode CurrentKeyCode;

    public void Tick()
    {
        if(_gameController.GameState == GameState.Game) Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(CurrentKeyCode))
        {
            _playerOne.Attack();
            _gameModeController.HitPlayerTwo(_settings.PlayerKeyValue);
            ChangeCurrentKeyCode();
        }
        else if(Input.anyKeyDown)
        {
            _incorrectKeyPressedSignal.Fire();
        }   
    }

    private void ChangeCurrentKeyCode()
    {
        CurrentKeyCode = _settings.KeyCodes[UnityEngine.Random.Range(0, _settings.KeyCodes.Length)];
    }

    [System.Serializable]
    public class Settings
    {
        public KeyCode[] KeyCodes;
        public float PlayerKeyValue;
    }
}
