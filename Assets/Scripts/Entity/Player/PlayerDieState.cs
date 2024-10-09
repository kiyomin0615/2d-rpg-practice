using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
