using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected Rigidbody2D playerRb;
    protected PlayerStateMachine playerStateMachine;
    protected bool animationEventTriggered;

    protected float stateTimer;
    protected float xInput;
    protected float yInput;
    private string animatorParam;

    public PlayerState(Player _player, PlayerStateMachine _playerStateMachine, string _animatorParam)
    {
        this.player = _player;
        this.playerStateMachine = _playerStateMachine;
        this.animatorParam = _animatorParam;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animatorParam, true);
        playerRb = player.rb;
        animationEventTriggered = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yVelocity", playerRb.velocity.y);
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animatorParam, false);
    }

    public virtual void TriggerAnimationEventOnState()
    {
        animationEventTriggered = true;
    }
}
