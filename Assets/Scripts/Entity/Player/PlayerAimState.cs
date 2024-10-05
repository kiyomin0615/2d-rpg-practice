using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SkillManager.instance.swordSkill.ToggleAimDots(true);
    }

    public override void Update()
    {
        base.Update();

        // Aim -> Throw(which has exit time) -> Idle
        if (Input.GetKeyUp(KeyCode.Mouse2))
            playerStateMachine.ChangeState(player.idleState);
    }   

    public override void Exit()
    {
        base.Exit();
    }
}
