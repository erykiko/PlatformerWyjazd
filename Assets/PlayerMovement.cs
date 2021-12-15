using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private bool canMove = true;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundRadius;

    [Header("Jumpin")]
    [SerializeField] private float jumpForce = 5f;

    [Header("Rollin")]
    [SerializeField] private bool isRolling = false;

    [Header("KeyBinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode rollKey = KeyCode.LeftShift;

    [Header("Combat")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private KeyCode attackKey = KeyCode.LeftControl;
    [SerializeField] private bool isBlocking;
    [SerializeField] private KeyCode blockKey = KeyCode.E;

    private Animator anim;
    private Rigidbody2D rb;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Jump();
        Roll();
        Attack();
        Block();
    }
    private void FixedUpdate()
    {
        Move();
        GroundCheck();
        
    }

    void Move()
    {
        if (!canMove)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("move", Mathf.Abs(moveX));

        if(!Mathf.Approximately(0f, moveX))
        {
            transform.rotation = moveX < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        transform.position += new Vector3(moveX, 0f, 0f) * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(jumpKey))
        {
            anim.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, layerMask);
        anim.SetBool("isJumping", !isGrounded);
    }
    
    void Roll()
    {
        if(!isRolling && Input.GetKeyDown(rollKey))
        {
            anim.SetTrigger("roll");
            isRolling = true;
            moveSpeed = moveSpeed * 1.5f;
        }
    }
    void EndRolling()
    {
        isRolling = false;
        moveSpeed = moveSpeed / 1.5f;
    }

    void Attack()
    {
        if(isGrounded && Input.GetKeyDown(attackKey))
        {
            canMove = false;
            isAttacking = true;
            anim.SetBool("isAttacking", isAttacking);
        }
        if (Input.GetKeyUp(attackKey))
        {
            canMove = true;
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
        }
    }
    void Block()
    {
        if (isGrounded && Input.GetKeyDown(blockKey))
        {
            canMove = false;
            isBlocking = true;
            anim.SetBool("isBlocking", isBlocking);
        }
        if (Input.GetKeyUp(blockKey))
        {
            canMove = true;
            isBlocking = false;
            anim.SetBool("isBlocking", isBlocking);
        }
    }
}
