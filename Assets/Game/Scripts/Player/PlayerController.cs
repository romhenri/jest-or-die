using System.Collections;
using UnityEngine;
using Platformer2D.Character;
using UnityEngine.SceneManagement;

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

    private int _lives = 3;
    private int _coins = 0;
    public int Lives => _lives;

    [Header("Platform")]
    [Tooltip("Assign the PlatformEffector2D manually if desired")]
    public PlatformEffector2D currentPlatformEffector;
    public float dropWaitTime = 0.2f;
    public float downwardForce = 10f;

    private bool isDroppingThroughPlatform = false;

    void Start()
    {
        playerMovement = GetComponent<CharacterMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();

        if (playerMovement == null)
        {
            Debug.LogError("CharacterMovement2D não encontrado no PlayerController.");
        }

        if (playerMovement.GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no PlayerController.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();

        if (transform.position.y < -8f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void HandleMovement()
    {
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(new Vector2(movementInput.x, 0));

        spriteRenderer.flipX = movementInput.x < 0;
        animator.SetBool("isWalking", movementInput.x != 0);
    }

    private void HandleJump()
    {
        if (playerInput.isJumpButtonDown())
        {
            playerMovement.Jump();
            AudioSource.PlayOneShot(audioJump);
        }

        if (!playerInput.isJumpButtonHeld())
        {
            playerMovement.UpdateJumpAbort();
        }
    }

    private void HandleCrouch()
    {
        if (playerInput.isCrouchButtonDown())
        {
            animator.SetBool("isCrounched", true);
            playerMovement.Crouch();

            if (currentPlatformEffector != null && playerInput.isDescendButtonHeld() && !isDroppingThroughPlatform)
            {
                StartCoroutine(DropThroughPlatform());
            }
        }
        else
        {
            animator.SetBool("isCrounched", false);
            playerMovement.UnCrouch();
        }
    }

    private IEnumerator DropThroughPlatform()
    {
        Rigidbody2D rb = playerMovement?.GetComponent<Rigidbody2D>();
        Collider2D platformCollider = currentPlatformEffector?.GetComponent<Collider2D>();

        isDroppingThroughPlatform = true;

        platformCollider.enabled = false;
        rb.AddForce(Vector2.down * downwardForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dropWaitTime);

        platformCollider.enabled = true;
        isDroppingThroughPlatform = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.CompareTag("Platform"))
        {
            PlatformEffector2D effector = collision.gameObject.GetComponent<PlatformEffector2D>();
            if (effector != null)
            {
                currentPlatformEffector = effector;
            }
        }

        if (collision.gameObject.CompareTag("Knife"))
        {
            Destroy(collision.gameObject);
            ReceiveDamage();
        }

        if (collision.gameObject.CompareTag("Coin"))
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

    public void ReceiveDamage()
    {
        _lives--;
        AudioSource.PlayOneShot(audioHurt);
        hudManager.DecreaseLives();

        if (_lives <= 0)
        {
            // Game Over
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
