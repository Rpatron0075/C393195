using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public float dashSpeed = 5f; // 대쉬 속도
    public float dashDistance = 10f; // 대쉬 거리
    public GameObject dashSprite; // 대쉬 스프라이트 오브젝트
    public GameObject idleSprite;
    public GameObject dashTrans;
    public GameObject idleTrans;


    private bool isDashing = false; // 대쉬 중인지 여부
    private Vector2 dashDirection; // 대쉬 방향
    private float dashStartTime; // 대쉬 시작 시간
    public Rigidbody2D rb; // Rigidbody2D 컴포넌트

    void Start()
    {
        rb = rb.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleDashMovement();
        DashSpriteTransUpdated();
    }

    public void StartDash(bool isFacingRight)
    {
        if (!isDashing)
        {
            // 대쉬 방향 설정
            dashDirection = isFacingRight ? Vector2.right : Vector2.left;
            isDashing = true;
            dashStartTime = Time.time;
            rb.velocity = dashDirection * dashSpeed;

            // 대쉬 스프라이트 활성화
            if (dashSprite != null)
            {
                dashSprite.SetActive(true);
                idleSprite.SetActive(false);
            }
        }
    }

    private void EndDash()
    {

    }

    private void HandleDashMovement()
    {
        if (isDashing)
        {
            if (!Input.GetKey(KeyCode.L) || Time.time - dashStartTime >= dashDistance / dashSpeed)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;

                // 대쉬 스프라이트 비활성화
                if (dashSprite != null)
                {
                    idleSprite.SetActive(true);
                    dashSprite.SetActive(false);
                }

                // 초기화
                dashDirection = Vector2.zero;
            }
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    private void DashSpriteTransUpdated()
    {
        dashTrans.transform.position = new Vector2(idleTrans.transform.position.x, idleTrans.transform.position.y);
    }
}
