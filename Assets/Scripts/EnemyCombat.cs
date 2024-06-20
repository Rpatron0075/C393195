using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombats : MonoBehaviour
{
    public List<Collider2D> attackColliders;  // 적의 공격 콜라이더들
    public float enemyDamage = 0.1f;
    public float enemyHealth = 500f;

    public PlayerCombats playerCombats;
    public PlayerController pc;
    public new Renderer renderer;
    private Enemy enemy;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach(Collider2D ac in playerCombats.attackColliders)
        {
            if (ac == other)
            {
                if (!pc.GetIsAttacked()) { return; }
                TakeDamage(playerCombats.playerDamage);
                return;
            }
        }
    }

    private void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        SetStunStack();
        StartCoroutine(ChangedRedColor());
        if (enemyHealth <= 0)
        {
            enemy.SetIsDied();
        }
    }

    IEnumerator ChangedRedColor()
    {
        yield return new WaitForSeconds(0.4f);
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        renderer.material.color = Color.white;
    }

    private void SetStunStack()
    {
        enemy.SetStunStack();
    }
}
