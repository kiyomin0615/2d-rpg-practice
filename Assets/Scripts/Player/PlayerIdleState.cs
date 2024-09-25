using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {


    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            return;

        if (xInput != 0 && !player.isDoingSomething)
            player.stateMachine.ChangeState(player.moveState);
    }   

    public override void Exit()
    {
        base.Exit();
    }
}
