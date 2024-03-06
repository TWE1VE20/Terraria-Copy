using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer[] render;
    [SerializeField] Animator animator;

    [Header("Property")]
    [SerializeField] float movePower;
    [SerializeField] float breakPower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    [SerializeField] float maxAnimSpeed;

    [SerializeField] float jumpSpeed;

    [SerializeField] LayerMask groundCheckLayer;

    private Vector2 moveDir;
    private bool isGround;
    private int groundCount;
    private float animeSpeed;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (moveDir.x < 0 && rigid.velocity.x > -maxXSpeed)
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower);
        }
        else if (moveDir.x > 0 && rigid.velocity.x < maxXSpeed)
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower);
        }
        else if (moveDir.x == 0 && rigid.velocity.x > 0)
        {
            rigid.AddForce(Vector2.left * breakPower);
        }
        else if (moveDir.x == 0 && rigid.velocity.x < 0)
        {
            rigid.AddForce(Vector2.right * breakPower);
        }

        if ((moveDir.x < 0 || moveDir.x > 0) && (rigid.velocity.x > 0 || rigid.velocity.x < 0))
            animator.SetFloat("XSpeed", maxAnimSpeed * Mathf.Abs((rigid.velocity.x / maxXSpeed)));
        else
            animator.SetFloat("XSpeed", 0);

        // 추락시 속도 제한
        if (rigid.velocity.y < -maxYSpeed)
        {
            Vector2 velocity = rigid.velocity;
            velocity.y = -maxYSpeed;
            rigid.velocity = velocity;
        }
    }

    private void Jump()
    {
        Vector2 velocity = rigid.velocity;
        velocity.y = jumpSpeed;
        rigid.velocity = velocity;
    }

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        if (moveDir.x < 0)
        {
            foreach (SpriteRenderer renderflip in render)
                renderflip.flipX = true;
            animator.SetBool("Move", true);
        }
        else if (moveDir.x > 0)
        {
            foreach (SpriteRenderer renderflip in render)
                renderflip.flipX = false;
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    private void OnJump(InputValue value)
    {
        if (isGround)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount++;
            isGround = groundCount > 0;
            animator.SetBool("isGround", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount--;
            isGround = groundCount > 0;
            animator.SetBool("isGround", false);
        }
    }
}
