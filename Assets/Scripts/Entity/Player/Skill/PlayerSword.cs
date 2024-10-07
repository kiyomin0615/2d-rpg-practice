using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    Player player;

    bool isStuck = false;
    bool isReturning = false;

    [SerializeField] float returnSpeed = 12;

    private void OnTriggerEnter2D(Collider2D other) {
        isStuck = true;

        circleCollider.enabled = false;

        rb.isKinematic = true; // Kinematic rigidbody is not affected by physics but could be triggered
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = other.transform;

        animator.SetBool("Fly", false);
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        player = PlayerManager.instance.player;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!isStuck)
            transform.right = rb.velocity;

        if (isReturning) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 0.2f) {
                player.ClearSword();
            }
        }
    }

    public void LaunchSword(Vector2 velocity, float gravity) {
        rb.velocity = velocity;
        rb.gravityScale = gravity;
        
        animator.SetBool("Fly", true);
    }

    public void GoBackToPlayer() {
        rb.isKinematic = false;

        transform.parent = null;

        isReturning = true;
    }
}
