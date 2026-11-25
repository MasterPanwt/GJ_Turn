using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine _stateMachine, PlayerController _playerController, string _animBoolName) : base(_stateMachine, _playerController, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        playerController.SetVelocity(playerController.moveInput * playerController.moveSpeed);
        if (playerController.moveInput == Vector2.zero)
        {
            stateMachine.ChangeState(playerController.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}