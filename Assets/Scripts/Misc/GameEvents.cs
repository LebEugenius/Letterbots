using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerOneWinSignal : Signal<PlayerOneWinSignal> { }
public class PlayerTwoWinSignal : Signal<PlayerTwoWinSignal> { }

public class PlayerOneRecoverSignal : Signal<PlayerOneRecoverSignal> { }
public class PlayerTwoRecoverSignal : Signal<PlayerTwoRecoverSignal> { }

public class PrepareSignal : Signal<PrepareSignal> { }
public class StartSignal : Signal<StartSignal> { }

public class IncorrectKeyPressedSignal : Signal<IncorrectKeyPressedSignal> { }
