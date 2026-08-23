using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jogador")]
    [SerializeField] private int playerId = 1;

    [Header("Controles")]
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float horizontal;
    private float lastDirection = 1f;

    private bool isGrounded;
    private bool isDashing;
    private bool canDash = true;

    // Guarda quais colliders estão servindo como chão
    private readonly HashSet<Collider2D> groundColliders = new();

    public int PlayerId => playerId;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Procura especificamente o filho chamado Visual
        Transform visual = transform.Find("Visual");

        if (visual != null)
        {
            animator = visual.GetComponent<Animator>();
            spriteRenderer = visual.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError(
                $"{name}: objeto filho 'Visual' não encontrado!"
            );
        }
    }

    private void Update()
    {
        ReadMovementInput();
        UpdateAnimations();
        UpdateDirection();
        HandleJump();
        HandleDash();
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;

        rb.linearVelocity = new Vector2(
            horizontal * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void ReadMovementInput()
    {
        horizontal = 0f;

        if (Input.GetKey(leftKey))
        {
            horizontal = -1f;
        }
        else if (Input.GetKey(rightKey))
        {
            horizontal = 1f;
        }
    }

    private void UpdateAnimations()
    {
        if (animator == null)
            return;

        animator.SetBool(
            "isMoving",
            Mathf.Abs(horizontal) > 0.01f
        );

        animator.SetFloat(
            "verticalVelocity",
            rb.linearVelocity.y
        );

        animator.SetBool(
            "isGrounded",
            isGrounded
        );
    }

    private void UpdateDirection()
    {
        if (horizontal == 0)
            return;

        lastDirection = Mathf.Sign(horizontal);

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = horizontal < 0;
        }
    }

    private void HandleJump()
    {
        if (!Input.GetKeyDown(jumpKey))
            return;

        Debug.Log(
            $"{name} tentou pular | Grounded: {isGrounded}"
        );

        if (!isGrounded)
            return;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );

        // Remove temporariamente o estado de chão.
        groundColliders.Clear();
        isGrounded = false;
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocity = new Vector2(
            lastDirection * dashSpeed,
            0f
        );

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundColliders.Remove(collision.collider);

        isGrounded = groundColliders.Count > 0;
    }

    private void CheckGroundCollision(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Normal apontando para cima = existe algo sob os pés
            if (contact.normal.y > 0.5f)
            {
                groundColliders.Add(collision.collider);
                isGrounded = true;
                return;
            }
        }
    }
}