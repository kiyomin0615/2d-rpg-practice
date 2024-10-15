using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchState : PlayerState
{
    Transform swordTransform;

    public PlayerCatchState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        swordTransform = player.thrownSword.transform;

        if ((player.transform.position.x > swordTransform.position.x && player.facingRight) || (player.transform.position.x < swordTransform.position.x && !player.facingRight))
        {
            player.Flip();
        }

        playerRb.velocity = new Vector2(player.knockbackSpeed * -player.facingDir, playerRb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (animationFinished)
        {
            playerStateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("WaitForDoingSomething", 0.15f);
    }
}
