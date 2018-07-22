using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Zenject;

public class SkirmishController : IGameMode
{
    public SkirmishController(
        Settings settings,
        PlayerOneWinSignal playerOneWinSignal,
        PlayerTwoWinSignal playerTwoWinSignal)
    {
        _settings = settings;
        _playerOneWinSignal = playerOneWinSignal;
        _playerTwoWinSignal = playerTwoWinSignal;
        ResetHealth();
    }

    private readonly Settings _settings;
    private readonly PlayerOneWinSignal _playerOneWinSignal;
    private readonly PlayerTwoWinSignal _playerTwoWinSignal;

    private float _playerOneHealth;
    private float _playerTwoHealth;

    public void HitPlayerOne(float value)
    {
        _playerOneHealth -= value;
        if (_playerOneHealth > 0) return;

        _playerTwoWinSignal.Fire();
        ResetHealth();
    }
    public void HitPlayerTwo(float value)
    {
        _playerTwoHealth -= value;
        if (_playerTwoHealth > 0) return;

        _playerOneWinSignal.Fire();
        ResetHealth();
    }

    public float GetBluePlayerHealth()
    {
        return _playerOneHealth;
    }
    public float GetRedPlayerHealth()
    {
        return _playerTwoHealth;
    }
    public float GetStartHealth()
    {
        return _settings.StartingHealth;
    }

    public void ResetHealth()
    {
        _playerOneHealth = _playerTwoHealth = _settings.StartingHealth;
    }

    [System.Serializable]
    public class Settings
    {
        public float StartingHealth;
    }
}
