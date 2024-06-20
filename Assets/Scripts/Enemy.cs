using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float detectionRange = 30.0f;
    public float attackTime = 4.0f;
    private float attackTimer;
    public float chargeRange = 20.0f;
    public float slashRange = 7.0f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public Transform playerTrans;
    private Rigidbody2D rb;
    private Animator animator;

    private float patrolTime = 3.0f;
    private float patrolTimer;
    private float alertTime = 2.0f;
    private float alertTimer;

    private bool isFacingRight = true;
    private bool isCharging = false;

    private float stunDuration = 3.0f;
    public float maxStunStack = 100f;
    private float stunStack;
    private bool isStunned = false;

    private bool isDied = false;

    private enum State
    {
        Patrol,
        Alert,
        Attack
    }
    private State currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not assigned!");
        }
        if (firePoint == null)
        {
            Debug.LogError("Fire Point is not assigned!");
        }
        if (playerTrans == null)
        {
            Debug.LogError("Player is not found!");
        }

        currentState = State.Patrol;
        patrolTimer = patrolTime;
        alertTimer = alertTime;
        attackTimer = attackTime;
    }

    void Update()
    {
        if (isDied) 
        { 
            StartCoroutine(Ending());
        }

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                SearchForPlayer();
                break;
            case State.Alert:
                Alert();
                SearchForPlayer();
                break;
            case State.Attack:
                float direction = isFacingRight ? 1 : -1;
                Attack(direction);
                break;
        }
    }

    void Patrol()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0)
        {
            patrolTimer = patrolTime;
            isFacingRight = !isFacingRight;
        }

        float direction = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        animator.SetTrigger("isWalked");
    }

    void SearchForPlayer()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, playerTrans.position);

        if (currentState == State.Patrol && distanceToPlayer < detectionRange)
        {
            currentState = State.Alert;
        }

        if (currentState == State.Alert)
        {
            if ((playerTrans.position.x > transform.position.x && !isFacingRight) ||
                (playerTrans.position.x < transform.position.x && isFacingRight))
            {
                Flip();
            }

            if (distanceToPlayer > chargeRange)
            {
                Chase();
            }

            if (attackTimer <= 0f)
            {
                currentState = State.Attack;
                attackTimer = attackTime;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    void Alert()
    {
        if (alertTimer <= 0)
        {
            alertTimer = alertTime;
            currentState = State.Attack;
        }

        float direction = isFacingRight ? 1 : -1;
        animator.SetTrigger("isWalked");

        if (Vector2.Distance(gameObject.transform.position, playerTrans.position) <= slashRange)
        {
            direction *= -1;
        }
        rb.velocity = new Vector2(direction * moveSpeed / 2, rb.velocity.y);
        alertTimer -= Time.deltaTime;
    }

    void Attack(float direction)
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, playerTrans.position);

        if (distanceToPlayer <= chargeRange)
        {
            if (distanceToPlayer <= slashRange * 2)
            {
                rb.velocity = new Vector2(direction * moveSpeed * 3f, rb.velocity.y);

                if (distanceToPlayer <= slashRange)
                {
                    animator.SetTrigger("isSlashAttacked");
                    StartCoroutine(SlashAttack());
                }
            }
            else
            {
                rb.velocity = new Vector2(-1 * direction * moveSpeed, rb.velocity.y);
                animator.SetTrigger("isChargingAttacked");
                StartCoroutine(ChargeAttack());
            }
        }
    }

    void Chase()
    {
        float direction = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * moveSpeed * 3f, rb.velocity.y);
        Debug.Log("Chase");
    }

    IEnumerator SlashAttack()
    {
        yield return new WaitForSeconds(1f);
        currentState = State.Alert;
    }

    IEnumerator ChargeAttack()
    {
        isCharging = true;

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.Initialize(playerTrans, this.gameObject);
            }
            else
            {
                Debug.LogError("Projectile component not found on instantiated object.");
            }
        }
        else
        {
            Debug.LogError("Projectile prefab or fire point is null.");
        }

        yield return new WaitForSeconds(3f);

        currentState = State.Alert;

        isCharging = false;
    }

    public void SetStunStack()
    {
        stunStack += 0.1f;
    }

    public void SetStun()
    {
        if (stunStack >= maxStunStack)
        {
            stunStack = 0f;
            isStunned = true;
        }
    }

    public void SetIsDied()
    {
        isDied = true;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    IEnumerator Ending()
    {
        animator.SetBool("isDied", isDied);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
