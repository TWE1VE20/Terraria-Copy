using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;

    [Header("Property")]
    [SerializeField] float movePower;
    [SerializeField] float breakPower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;

    [SerializeField] float jumpSpeed;

    private Vector2 moveDir;
    private bool isGround;

    private void FixedUpdate()
    {
        Move();
        animator.SetBool("Jump", false);
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
            animator.SetBool("Run", true);
            render.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            animator.SetBool("Run", true);
            render.flipX = false;
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void OnJump(InputValue value)
    {
        if (isGround)
        {
            animator.SetBool("Jump", true);
            Jump();
        }
    }
}
