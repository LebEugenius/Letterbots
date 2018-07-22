using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<PlayerOneWinSignal>();
        Container.DeclareSignal<PlayerTwoWinSignal>();

        Container.DeclareSignal<PlayerOneRecoverSignal>();
        Container.DeclareSignal<PlayerTwoRecoverSignal>();

        Container.DeclareSignal<PrepareSignal>();
        Container.DeclareSignal<StartSignal>();

        Container.DeclareSignal<IncorrectKeyPressedSignal>();
    }
}
