using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage = 1;
    public bool active = true;
    public bool isLooping = false;
    public float timing = 1.0f;

    private Animator animator;
    private Collider2D spikeCollider;
    private float timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spikeCollider = GetComponent<Collider2D>();

        if (isLooping)
        {
            timer = timing;
        }
        UpdateSpikeState();
    }

    private void Update()
    {
        if (isLooping)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                active = !active;
                UpdateSpikeState();
                timer = timing;
            }
        }
    }

    private void UpdateSpikeState()
    {
        animator.SetBool("isActive", active);
        spikeCollider.enabled = active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active && other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.ReceiveDamage();

                Vector2 contactPoint = transform.position;
                playerController.SpawnBlood(contactPoint);
            }
        }
    }
}
