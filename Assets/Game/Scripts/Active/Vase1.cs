using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    private Animator animator;
    private Collider2D collider;
    private AudioSource audioSource;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject bombPrefab;

    [Range(0, 1)][SerializeField] private float coinChance = 0.5f;
    [Range(0, 1)][SerializeField] private float bombChance = 0.2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (
           collision.gameObject.CompareTag("Player") ||
           collision.gameObject.CompareTag("Knife")
        )
        {
            TriggerBreakAnimation();
            collider.enabled = false;
        }
    }

    void TriggerBreakAnimation()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        animator.SetTrigger("Break");
    }

    public void OnBreakAnimationEnd()
    {
        DropItem();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    void DropItem()
    {
        float randomValue = Random.value;

        if (randomValue <= coinChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue <= coinChance + bombChance && bombPrefab != null)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }
}
