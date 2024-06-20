using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public float dashSpeed = 5f; // �뽬 �ӵ�
    public float dashDistance = 10f; // �뽬 �Ÿ�
    public GameObject dashSprite; // �뽬 ��������Ʈ ������Ʈ
    public GameObject idleSprite;
    public GameObject dashTrans;
    public GameObject idleTrans;


    private bool isDashing = false; // �뽬 ������ ����
    private Vector2 dashDirection; // �뽬 ����
    private float dashStartTime; // �뽬 ���� �ð�
    public Rigidbody2D rb; // Rigidbody2D ������Ʈ

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
            // �뽬 ���� ����
            dashDirection = isFacingRight ? Vector2.right : Vector2.left;
            isDashing = true;
            dashStartTime = Time.time;
            rb.velocity = dashDirection * dashSpeed;

            // �뽬 ��������Ʈ Ȱ��ȭ
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

                // �뽬 ��������Ʈ ��Ȱ��ȭ
                if (dashSprite != null)
                {
                    idleSprite.SetActive(true);
                    dashSprite.SetActive(false);
                }

                // �ʱ�ȭ
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
