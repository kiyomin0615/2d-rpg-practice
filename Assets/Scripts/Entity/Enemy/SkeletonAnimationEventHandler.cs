using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationEventHandler : MonoBehaviour
{
    private Enemy enemyComponent;

    public void Awake()
    {
        enemyComponent = GetComponentInParent<Enemy>();
    }

    private void HandleAnimationEvent()
    {
        enemyComponent.OnAnimationEvent();
    }
}
