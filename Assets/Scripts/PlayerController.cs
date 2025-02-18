using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _characterBody;
    [SerializeField] private float _movementSpeed;
    private Rigidbody2D _rb;
    private bool isDead = false; // Track if player is dead

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead) // Allow movement only if not dead
        {
            HandlePlayerMovement();
        }

        // Toggle death mode when pressing "K"
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isDead)
            {
                Die();
            }
            else
            {
                Revive();
            }
        }
    }

    private void HandlePlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * _movementSpeed;
        _rb.velocity = movement;

        bool characterIsWalking = movement.magnitude > 0f;
        _animator.SetBool("isWalking", characterIsWalking);

        bool flipSprite = movement.x < 0f;
        _characterBody.flipX = flipSprite;
    }

    private void Die()
    {
        isDead = true;
        _animator.SetTrigger("Die"); // Use the trigger to transition to PlayerDie
        _rb.velocity = Vector2.zero; // Stop movement
    }

    private void Revive()
    {
        isDead = false;
        _animator.Play("PlayerIdle"); // Reset animation manually
    }
}
