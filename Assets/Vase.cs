using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with vase");
        TriggerBreakAnimation();

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with vase");
        }

    }

    void TriggerBreakAnimation()
    {
        animator.SetTrigger("Break");
    }

    // Este método será chamado pelo evento de animação
    public void OnBreakAnimationEnd()
    {
        Destroy(gameObject);
    }
}