using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
