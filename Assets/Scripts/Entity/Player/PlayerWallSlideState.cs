using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _playerStateMachine, string _animatorParam) : base(_player, _playerStateMachine, _animatorParam)
    {   
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (yInput < 0)
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        else
            playerRb.velocity = new Vector2(0, playerRb.velocity.y * 0.7f);

        if (Input.GetKeyDown(KeyCode.Space))
            playerStateMachine.ChangeState(player.wallJumpState);

        if ((xInput != 0 && player.facingDir != xInput) || player.IsGroundDetected())
            playerStateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    
}
