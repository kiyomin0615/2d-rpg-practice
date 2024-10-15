using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animatorParam) : base(_player, _playerStateMachine, _animatorParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(playerRb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (playerRb.velocity.y < 0)
        {
            playerStateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
