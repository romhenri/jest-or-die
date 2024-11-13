using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vase : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("Start method called. Animator component initialized.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void broke()
    {
        Debug.Log("broke method called. Triggering Break animation.");
        animator.SetTrigger("Break");
        // Opcional: Desativar o objeto após a animação
        // this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
        {
            Debug.Log("Collision is null.");
            return;
        }

        Debug.Log("OnCollisionEnter2D method called. Collision detected with: " + collision.gameObject.name);
        this.broke();
    }
}