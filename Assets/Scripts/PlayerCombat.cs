using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombats : MonoBehaviour
{
    public List<Collider2D> attackColliders;  // �÷��̾��� ���� �ݶ��̴�
    public float playerDamage = 100;             // �÷��̾ ������ �ִ� ������
    public float playerHealth = 1000;            // �÷��̾��� ü��

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
            // �÷��̾ �׾��� �� ó��
            Debug.Log("Player Died");
        }
    }
}
