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
    public float maxLivingTime = 5f; // ����ü�� �ִ� ���� �ð� (��: 5��)
    private EnemyCombats enemyCombats;

    public void Initialize(Transform playerTransform, GameObject enemy)
    {
        collider = GetComponent<Collider2D>();
        enemyCombats = enemy.GetComponent<EnemyCombats>();

        player = playerTransform;
        enemyCombats.attackColliders.Add(collider);
        Invoke("Launch", 3f); // 3�� �� �߻�

        // �ִ� ���� �ð� �Ŀ� �ڵ����� �ı��ǵ��� ����
        Invoke("DestroyProjectile", maxLivingTime);
    }

    void Launch()
    {
        if (player != null)
        {
            // ��ǥ ������ ����Ͽ� ���� ���ͷ� ����ϴ�.
            targetDirection = (player.position - transform.position).normalized;
            isLaunched = true;
        }
    }

    void Update()
    {
        if (isLaunched)
        {
            // ��ǥ �������� ����ü�� �̵���ŵ�ϴ�.
            transform.position += targetDirection * speed * Time.deltaTime;
        }
    }

    void DestroyProjectile()
    {
        enemyCombats.attackColliders.Remove(collider);
        // ���� �ð��� ������ ����ü �ı�
        Destroy(gameObject);
    }
}
