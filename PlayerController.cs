using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    
    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Fisika
        rb.gravityScale = 3f; 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }

    void Update()
    {
        // Pastikan keyboard tidak error
        if (Keyboard.current == null) return;

        // Input A/D dan Panah Kiri/Kanan dengan sistem baru
        horizontalInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            horizontalInput = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            horizontalInput = 1f;

        // Cek pijakan tanah
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Lompat dengan Spacebar
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            // Menggunakan velocity (bukan linearVelocity)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Cek jatuh ke jurang (Aman dari error jika GameManager belum ada)
        if (transform.position.y < -10f && GameManager.Instance != null) 
        {
            GameManager.Instance.GameOver();
        }
    }

    void FixedUpdate()
    {
        // Menggerakkan karakter menggunakan velocity (bukan linearVelocity)
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance == null) return;

        // Tabrakan jebakan merah atau finish
        if (collision.CompareTag("RedZone"))
        {
            GameManager.Instance.GameOver();
        }
        else if (collision.CompareTag("Goal"))
        {
            GameManager.Instance.LevelComplete();
        }
    }
}