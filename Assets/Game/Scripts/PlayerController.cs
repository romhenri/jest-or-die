using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    CharacterMovement2D playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;

    public Animator animator;

    public Sprite defaultSprite;
    //public Sprite crounchedSprite;

    void Start()
    {
        playerMovement = GetComponent<CharacterMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(new Vector2(movementInput.x, 0));

        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        } else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (movementInput.x != 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }

        // Jump
        if (playerInput.isJumpButtonDown())
        {
            Debug
                .Log("Jump");
            playerMovement.Jump();
        }
        if (playerInput.isJumpButtonHeld() == false)
        {
            playerMovement.UpdateJumpAbort();
        }

        // Crouch (Disabled)
        if (playerInput.isCrouchButtonDown())
        {
            playerMovement.Crouch();
            //spriteRenderer.sprite = crounchedSprite;
        }
        if (playerInput.isCrouchButtonDown() == false)
        {
            playerMovement.UnCrouch();
            spriteRenderer.sprite = defaultSprite;
        }

        // Restart if fall
        if (transform.position.y < -8f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

    }
    public static void hit()
    {
        Destroy(GameObject.Find("Player"));
        Task.Delay(100);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
