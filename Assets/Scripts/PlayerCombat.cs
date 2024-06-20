using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombats : MonoBehaviour
{
    public List<Collider2D> attackColliders;  // 플레이어의 공격 콜라이더
    public float playerDamage = 100;             // 플레이어가 적에게 주는 데미지
    public float playerHealth = 1000;            // 플레이어의 체력

    public EnemyCombats enemyCombats;

    private void OnTriggerStay2D(Collider2D other)
    {
        foreach (Collider2D ac in enemyCombats.attackColliders)
        {
            if (ac == other)
            {
                TakeDamage(enemyCombats.enemyDamage);
                break;
            }
        }
    }

    private void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            // 플레이어가 죽었을 때 처리
            Debug.Log("Player Died");
        }
    }
}
