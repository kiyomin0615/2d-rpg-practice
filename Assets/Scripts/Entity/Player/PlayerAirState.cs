using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _playerStateMachine, string _animatorParam) : base(_player, _playerStateMachine, _animatorParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * xInput * 0.8f, playerRb.velocity.y);

        if (player.IsGroundDetected())
            playerStateMachine.ChangeState(player.idleState);

        if (player.IsWallDetected())
            playerStateMachine.ChangeState(player.wallSlideState);


    }

    public override void Exit()
    {
        base.Exit();
    }
}
