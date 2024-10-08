using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState: PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animatorParam): base(_player, _playerStateMachine, _animatorParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        SkillManager.instance.cloneSkill.CreateClone(player.transform.position, Vector2.zero);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            playerStateMachine.ChangeState(player.idleState);

        if (!player.IsGroundDetected() && player.IsWallDetected())
            playerStateMachine.ChangeState(player.wallSlideState);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0,  playerRb.velocity.y);
    }


}
