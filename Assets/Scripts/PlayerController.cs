using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;  // 플레이어 오브젝트를 할당할 변수
    public float moveSpeed = 100f;
    public float dashSpeed = 20f;
    public float dashDistance = 3f;
    private bool isJumping = false;
    private bool isFacingRight = false;  // 초기 값 false로 설정하여 플레이어가 처음에는 왼쪽을 바라보도록 설정
    private Vector2 dashDirection;
    private float dashStartTime;
    private float jumpCoolTime = 0f;
    public float maxJumpCoolTime = 3f;

    public Rigidbody2D rb;
    public Animator animator;
    public DashController c_dash;
    public PlayerCombats combat;

    private bool isAttacked = false;
    private Collider2D currentAttackbox;

    void Start()
    {
        if (player != null)
        {
            combat = combat.GetComponent<PlayerCombats>();
            rb = rb.GetComponent<Rigidbody2D>();
            animator = animator.GetComponent<Animator>();
            c_dash = c_dash.GetComponent<DashController>();

            if (isFacingRight)
            {
                Flip();  // 초기 방향 설정이 오른쪽이면 Flip 호출
            }
        }
        else
        {
            Debug.LogError("Player object is not assigned.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttackAndDash();
    }

    void HandleMovement()
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) return;

        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            if (isFacingRight)
            {
                Flip();
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveX = 0f;
                return;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            if (!isFacingRight)
            {
                Flip();
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveX = 0f;
                return;
            }   
                
        }

        Vector2 movement = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
        animator.SetTrigger("isWalked");
    }

    void HandleJump()
    {
        if (jumpCoolTime < maxJumpCoolTime)
        {
            jumpCoolTime += 0.1f;
        }

        jumpCoolTime = 0f;

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("isJumped");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetTrigger("isSitDown");
        }
    }

    public void SetHitBoxActive()
    {
        isAttacked = true;
    }
    public void SetHitBoxNotActive()
    {
        isAttacked = false;
    }

    void HandleAttackAndDash()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PerformAttack();
        }

        if (!Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L))
        {
            c_dash.StartDash(isFacingRight);
        }
    }

    void PerformAttack()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Perform Jump Attack
            animator.SetTrigger("1");
            //currentAttackbox = combat.attackColliders[1];
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // Perform Type 1 Attack
            animator.SetTrigger("2");
            //currentAttackbox = combat.attackColliders[2];
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Perform Ground Attack
            animator.SetTrigger("3");
            //currentAttackbox = combat.attackColliders[3];
        }
        else if (Input.GetKeyDown(KeyCode.L)) 
        {
            animator.SetTrigger("4");
            //currentAttackbox = combat.attackColliders[4];
        }
        else
        {
            // Perform Normal Attack
            animator.SetTrigger("0");
            currentAttackbox = combat.attackColliders[0];
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = player.transform.localScale;
        scaler.x *= -1;
        player.transform.localScale = scaler;
    }

    public bool GetIsAttacked()
    {
        return isAttacked;
    }

    public Collider2D GetAttackCollider()
    {
        if (currentAttackbox == null) return null;

        return currentAttackbox;
    }
}
