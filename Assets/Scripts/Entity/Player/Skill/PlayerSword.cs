using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    Player player;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetupSword(Vector2 velocity, float gravity) {
        rb.velocity = velocity;
        rb.gravityScale = gravity;
    }
}
