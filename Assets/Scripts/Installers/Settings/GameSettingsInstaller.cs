using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private AIController.Settings _aiController;
    [SerializeField] private InputController.Settings _inputController;
    [SerializeField] private SkirmishController.Settings _skirmishSettings;
    [SerializeField] private GameController.Settings _gameSettings;

    public override void InstallBindings()
    {
        Container.BindInstances(
            _aiController,
            _inputController,
            _skirmishSettings,
            _gameSettings);
    }
}
