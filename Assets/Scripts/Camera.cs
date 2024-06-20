using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public Vector3 offset;  // 카메라의 오프셋
    public float normalSmoothSpeed = 0.125f;  // 일반적인 따라오는 속도
    public float dashSmoothSpeed = 0.05f;  // 대쉬 시 따라오는 속도

    public float shakeDuration = 0.2f;  // 흔들림 지속 시간
    public float shakeMagnitude = 0.3f;  // 흔들림 강도

    private float currentSmoothSpeed;  // 현재 따라오는 속도

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
        // 대쉬 상태를 확인하는 로직 (임시로 bool 값으로 대체)
        bool isDashing = Input.GetKey(KeyCode.L);  // 대쉬 키를 누르고 있는지 확인

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
