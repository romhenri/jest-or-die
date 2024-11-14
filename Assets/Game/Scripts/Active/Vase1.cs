using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    private Animator animator;
    private Collider2D collider;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerBreakAnimation();

        collider.enabled = false;
    }

    void TriggerBreakAnimation()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        animator.SetTrigger("Break");
    }

    public void OnBreakAnimationEnd()
    {
        Destroy(gameObject);
    }
}
