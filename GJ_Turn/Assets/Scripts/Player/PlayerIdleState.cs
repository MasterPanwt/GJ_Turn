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
        playerController.anim.speed = 0f;
        AnimatorStateInfo stateInfo = playerController.anim.GetCurrentAnimatorStateInfo(0);
        playerController.anim.Play(stateInfo.fullPathHash, 0, 0.99f);
    }

    public override void Update()
    {
        base.Update();
        if (playerController.moveInput != Vector2.zero)
        {
            stateMachine.ChangeState(playerController.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        playerController.anim.speed = 1f;
    }
}