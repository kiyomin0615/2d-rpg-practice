using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCounter;
    private float lastAttackTime;
    private float comboDuration = 2f;

    public PlayerAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(2);

        xInput = 0;

        if (comboCounter > 2 || Time.time >= lastAttackTime + comboDuration)
            comboCounter = 0;

        player.animator.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        player.SetVelocity(player.attackVelocityList[comboCounter].x * attackDir, player.attackVelocityList[comboCounter].y);

        stateTimer = 0.1f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetVelocity(0, 0);

        if (animationFinished)
            playerStateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("WaitForDoingSomething", 0.15f);

        lastAttackTime = Time.time;
        comboCounter++;
    }
}
