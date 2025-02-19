using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIState
{
    Home,
    Game,
    GameOver
}

public abstract class BaseUIManager : MonoBehaviour
{
    protected UIState currentState;

    protected abstract void Awake();

    public abstract void SetPlayGame();

    public abstract void SetGameOver();

    public abstract void ChangeState(UIState state);
}
