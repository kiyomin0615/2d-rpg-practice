using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _playerStateMachine, string _animatorParam) : base(_player, _playerStateMachine, _animatorParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            playerStateMachine.ChangeState(player.attackState);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            playerStateMachine.ChangeState(player.counterAttackState);

        if (Input.GetKeyDown(KeyCode.Mouse2) && player.HasSword())
            playerStateMachine.ChangeState(player.aimState);

        if (!player.IsGroundDetected())
            playerStateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            playerStateMachine.ChangeState(player.jumpState);

        if (Input.GetKeyDown(KeyCode.R))
            playerStateMachine.ChangeState(player.ultimateState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
