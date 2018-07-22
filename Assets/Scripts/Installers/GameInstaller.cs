using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerFacade _playerOne;
    [SerializeField] private PlayerFacade _playerTwo;
    [SerializeField] private UIManager _uiManager;

    public override void InstallBindings()
    {   
        Container.BindSignal<IncorrectKeyPressedSignal>().To<UIManager>(x => x.OnIncorrectKeyPressed).FromInstance(_uiManager).AsSingle();
        Container.BindInterfacesAndSelfTo<SkirmishController>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();     
        Container.BindInterfacesAndSelfTo<AIController>().AsSingle().WithArguments(_playerTwo);
        Container.BindInterfacesAndSelfTo<InputController>().AsSingle().WithArguments(_playerOne);

        GameSignalsInstaller.Install(Container);
        
        Container.BindSignal<PlayerOneWinSignal>().To<PlayerFacade>(x => x.OnVictory).FromInstance(_playerOne).AsCached();
        Container.BindSignal<PlayerTwoWinSignal>().To<PlayerFacade>(x => x.OnDefeat).FromInstance(_playerOne).AsCached();
        Container.BindSignal<PlayerOneRecoverSignal>().To<PlayerFacade>(x => x.OnRecover).FromInstance(_playerOne).AsCached();
        Container.BindSignal<PrepareSignal>().To<PlayerFacade>(x => x.OnPrepare).FromInstance(_playerOne).AsCached();
        
        Container.BindSignal<PlayerOneWinSignal>().To<PlayerFacade>(x => x.OnDefeat).FromInstance(_playerTwo).AsCached();
        Container.BindSignal<PlayerTwoWinSignal>().To<PlayerFacade>(x => x.OnVictory).FromInstance(_playerTwo).AsCached();
        Container.BindSignal<PlayerTwoRecoverSignal>().To<PlayerFacade>(x => x.OnRecover).FromInstance(_playerTwo).AsCached();
        Container.BindSignal<PrepareSignal>().To<PlayerFacade>(x => x.OnPrepare).FromInstance(_playerTwo).AsCached();

        Container.BindSignal<PlayerOneWinSignal>().To<GameController>(x => x.OnPlayerOneWin).AsSingle();
        Container.BindSignal<PlayerTwoWinSignal>().To<GameController>(x => x.OnPlayerTwoWin).AsSingle();
    }
}
