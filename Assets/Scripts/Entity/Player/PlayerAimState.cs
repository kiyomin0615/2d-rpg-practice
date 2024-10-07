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

        player.SetVelocity(0f, 0f);

        // Aim -> Throw(which has exit time) -> Idle
        if (Input.GetKeyUp(KeyCode.Mouse2))
            playerStateMachine.ChangeState(player.idleState);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((player.transform.position.x > mousePosition.x && player.facingRight) || (player.transform.position.x < mousePosition.x && !player.facingRight))
        {
            player.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("WaitForDoingSomething", 0.15f);
    }
}
