using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator animator;

    public string uid;
    public bool isActive = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu("Generate an UID")]
    void GererateCheckpointUID()
    {
        uid = System.Guid.NewGuid().ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && !isActive)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        isActive = true;
        animator.SetBool("isActive", true);
        AudioManager.instance.PlaySFX(5, transform);
    }
}
