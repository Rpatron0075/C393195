using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform player;
    private Vector3 targetDirection;
    public float speed = 5f;
    private bool isLaunched = false;
    private new Collider2D collider;
    public float maxLivingTime = 5f; // 투사체의 최대 생존 시간 (예: 5초)
    private EnemyCombats enemyCombats;

    public void Initialize(Transform playerTransform, GameObject enemy)
    {
        collider = GetComponent<Collider2D>();
        enemyCombats = enemy.GetComponent<EnemyCombats>();

        player = playerTransform;
        enemyCombats.attackColliders.Add(collider);
        Invoke("Launch", 3f); // 3초 후 발사

        // 최대 생존 시간 후에 자동으로 파괴되도록 설정
        Invoke("DestroyProjectile", maxLivingTime);
    }

    void Launch()
    {
        if (player != null)
        {
            // 목표 방향을 계산하여 단위 벡터로 만듭니다.
            targetDirection = (player.position - transform.position).normalized;
            isLaunched = true;
        }
    }

    void Update()
    {
        if (isLaunched)
        {
            // 목표 방향으로 투사체를 이동시킵니다.
            transform.position += targetDirection * speed * Time.deltaTime;
        }
    }

    void DestroyProjectile()
    {
        enemyCombats.attackColliders.Remove(collider);
        // 일정 시간이 지나면 투사체 파괴
        Destroy(gameObject);
    }
}
