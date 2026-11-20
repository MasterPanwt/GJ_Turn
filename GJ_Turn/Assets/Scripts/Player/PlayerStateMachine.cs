using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState curState { get; private set; }
    
    public void Init(PlayerState _state)
    {
        curState = _state;
        curState.Enter();
    }

    public void ChangeState(PlayerState _state)
    {
        curState.Exit();
        curState = _state;
        curState.Enter();
    }
}