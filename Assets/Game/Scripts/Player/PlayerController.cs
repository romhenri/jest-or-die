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
    public Sprite defaultSprite;

    CharacterMovement2D playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;

    public Animator animator;
    public HudManager hudManager;
    public CanvasController canvasController;

    [Header("Audio")]
    public AudioSource AudioSource;
    public AudioClip audioJump;
    public AudioClip audioHurt;
    public AudioClip audioGet;
    //public Sprite crounchedSprite;

    private int _lives = 3;
    private int _coins = 0;
    // This is a property that can be accessed from other scripts but not modified
    public int Lives { get => _lives; }

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
            playerMovement.Jump();
            AudioSource.PlayOneShot(audioJump);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void ReceiveDamage()
    {
        _lives--;
        AudioSource.PlayOneShot(audioHurt);
        if (_lives <= 0)
        {
            // Game Over
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        hudManager.DecreaseLives();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.tag == "Knife")
        {
            Destroy(collision.gameObject);
            ReceiveDamage();
        }
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            AudioSource.PlayOneShot(audioGet);
            _coins++;

            if (_coins == 4)
            {
                canvasController.SetWinScreen();
            }
        }
    }
}
