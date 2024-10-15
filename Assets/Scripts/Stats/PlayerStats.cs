using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    Player playerComponent;

    protected override void Start()
    {
        base.Start();

        playerComponent = PlayerManager.instance.player;
    }

    public override void ReduceHP(Entity subject)
    {
        base.ReduceHP(subject);
    }

    protected override void Die()
    {
        base.Die();

        playerComponent.Die();
    }
}
