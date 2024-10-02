using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    private Player playerComponent;

    public void Awake()
    {
        playerComponent = GetComponentInParent<Player>();
    }

    private void HandleAnimationEvent()
    {
        playerComponent.OnAnimationEvent();
    }
}
