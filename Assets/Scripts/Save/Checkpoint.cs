using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator animator;

    public string uid;
    public bool isActive = false;

    void Start()
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
        if (player != null)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        isActive = true;
        animator.SetBool("isActive", true);
    }
}
