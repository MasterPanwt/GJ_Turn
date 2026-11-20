using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController playerController;
    private string animBoolName;
    protected Vector2 moveInput;
    
    public PlayerState(PlayerStateMachine _stateMachine, PlayerController _playerController, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.playerController = _playerController;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public virtual void Exit()
    {

    }
}