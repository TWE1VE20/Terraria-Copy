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
    [SerializeField] GameObject[] fliper;
    [SerializeField] Animator animator;

    [Header("Property")]
    [SerializeField] float movePower;
    [SerializeField] float breakPower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxAnimSpeed;

    [SerializeField] float jumpSpeed;
    [SerializeField] float maxJumpTime;
    [SerializeField] float maxYSpeed;

    public bool isGround;

    private Vector2 moveDir;
    private bool isJumping;
    private Coroutine jumpCoroutine;


    private void FixedUpdate()
    {
        Move();
        if(isGround)
            animator.SetBool("isGround", true);
        else
            animator.SetBool("isGround", false);
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
        Flip(moveDir.x);
        if (moveDir.x < 0)
            animator.SetBool("Move", true);
        else if (moveDir.x > 0)
            animator.SetBool("Move", true);
        else
            animator.SetBool("Move", false);
    }

    public void Flip(float Dir)
    {
        if (Dir < 0)
        {
            foreach (SpriteRenderer renderflip in render)
                renderflip.flipX = true;
            foreach (GameObject scaleflip in fliper)
                scaleflip.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Dir > 0)
        {
            foreach (SpriteRenderer renderflip in render)
                renderflip.flipX = false;
            foreach (GameObject scaleflip in fliper)
                scaleflip.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
            if (isGround)
                jumpCoroutine = StartCoroutine(JumpCoroutine());

        if (!value.isPressed)
            if (jumpCoroutine != null)
                StopCoroutine(jumpCoroutine);

    }

    IEnumerator JumpCoroutine()
    {
        isJumping = true;

        float jumpTime = 0f;

        while (jumpTime < maxJumpTime)
        {
            // 점프 버튼을 누른 길이만큼 점프력 조절
            float jumpPower = Input.GetAxis("Jump") * jumpSpeed;

            // 점프력 적용
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);

            jumpTime += Time.deltaTime;

            yield return null;
        }

        isJumping = false;
    }
}
