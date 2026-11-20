using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine _stateMachine, PlayerController _playerController, string _animBoolName) : base(_stateMachine, _playerController, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (moveInput != Vector2.zero)
        {
            stateMachine.ChangeState(playerController.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}