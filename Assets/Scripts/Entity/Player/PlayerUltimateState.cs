using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerUltimateState : PlayerState
{
    float originalGravityScale;
    float flyTime = 0.4f;
    bool usingSkill = false;

    public PlayerUltimateState(Player _player, PlayerStateMachine _playerStateMachine, string _animatorParam): base(_player, _playerStateMachine, _animatorParam)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = flyTime;
        usingSkill = false;
        originalGravityScale = playerRb.gravityScale;
        playerRb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) {
            playerRb.velocity = new Vector2(0, 15);
        }

        if (stateTimer <= 0) {
            playerRb.velocity = new Vector2(0, -0.1f);
            if (!usingSkill) {
                if(SkillManager.instance.ultimateSkill.TrySkill())
                    usingSkill = true;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        playerRb.gravityScale = originalGravityScale;
        player.Appear();
    }
}
