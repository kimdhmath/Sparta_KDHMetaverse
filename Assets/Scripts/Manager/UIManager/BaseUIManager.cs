using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIState
{
    Home,
    Game,
    GameOver,
    None
}

public abstract class BaseUIManager : MonoBehaviour
{
    protected UIState currentState;

    public abstract void ChangeState(UIState state);
}
