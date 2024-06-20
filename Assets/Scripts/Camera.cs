using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public Vector3 offset;  // ī�޶��� ������
    public float normalSmoothSpeed = 0.125f;  // �Ϲ����� ������� �ӵ�
    public float dashSmoothSpeed = 0.05f;  // �뽬 �� ������� �ӵ�

    public float shakeDuration = 0.2f;  // ��鸲 ���� �ð�
    public float shakeMagnitude = 0.3f;  // ��鸲 ����

    private float currentSmoothSpeed;  // ���� ������� �ӵ�

    private void Start()
    {
        currentSmoothSpeed = normalSmoothSpeed;
    }

    private void LateUpdate()
    {
        HandleCameraFollow();
    }

    private void HandleCameraFollow()
    {
        // �뽬 ���¸� Ȯ���ϴ� ���� (�ӽ÷� bool ������ ��ü)
        bool isDashing = Input.GetKey(KeyCode.L);  // �뽬 Ű�� ������ �ִ��� Ȯ��

        if (isDashing)
        {
            currentSmoothSpeed = dashSmoothSpeed;
        }
        else
        {
            currentSmoothSpeed = normalSmoothSpeed;
        }

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, currentSmoothSpeed);
        transform.position = smoothedPosition;
    }

    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
