using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage = 1; // Quantidade de dano que o espinho causa (ajuste conforme necess�rio)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu � o jogador, comparando a tag
        if (other.CompareTag("Player"))
        {
            // Obt�m o componente PlayerController do jogador
            var playerController = other.GetComponent<PlayerController>();

            // Se o jogador tem o componente PlayerController, aplica o dano
            if (playerController != null)
            {
                // Chama a fun��o ReceiveDamage no jogador
                playerController.ReceiveDamage();
            }
        }
    }
}
