using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public Transform p_trans; // 플레이어의 Transform
    public Transform[] grounds; // 3개의 그라운드를 담을 배열

    private Transform centralGround;
    private Transform farthestGround;

    private float groundWidth; // 그라운드의 폭

    void Start()
    {
        // 그라운드의 폭을 계산합니다. 모든 그라운드가 같은 폭을 가진다고 가정합니다.
        groundWidth = grounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // 플레이어의 위치를 기준으로 중앙 그라운드를 찾습니다.
        centralGround = GetCentralGround();

        // 가장 멀리 있는 그라운드를 찾습니다.
        farthestGround = GetFarthestGround(centralGround);

        // 플레이어의 위치에 따라 그라운드를 재배치합니다.
        RepositionGrounds(centralGround, farthestGround);
    }

    Transform GetCentralGround()
    {
        // 플레이어와 가장 가까운 그라운드를 찾습니다.
        Transform centralGround = grounds[0];
        float minDistance = Mathf.Abs(p_trans.position.x - centralGround.position.x);

        for (int i = 1; i < grounds.Length; i++)
        {
            float distance = Mathf.Abs(p_trans.position.x - grounds[i].position.x);
            if (distance < minDistance)
            {
                centralGround = grounds[i];
                minDistance = distance;
            }
        }

        return centralGround;
    }

    Transform GetFarthestGround(Transform centralGround)
    {
        // 중앙 그라운드를 제외하고 가장 멀리 있는 그라운드를 찾습니다.
        Transform farthestGround = null;
        float maxDistance = float.MinValue;

        foreach (Transform ground in grounds)
        {
            if (ground == centralGround) continue;

            float distance = Mathf.Abs(p_trans.position.x - ground.position.x);
            if (distance > maxDistance)
            {
                farthestGround = ground;
                maxDistance = distance;
            }
        }

        return farthestGround;
    }

    void RepositionGrounds(Transform centralGround, Transform farthestGround)
    {
        foreach (Transform ground in grounds)
        {
            if (ground == farthestGround || ground == centralGround) continue;

            float criterion = centralGround.position.x + ground.position.x / 2f;    // 가장 먼 그라운드의 배치 위치를 판단할 x 기준점

            if (p_trans.position.x - criterion > 0) // 오른쪽
            {
                farthestGround.position = new Vector3(centralGround.position.x + groundWidth, farthestGround.position.y, farthestGround.position.z);
            }
            else if (p_trans.position.x - criterion < 0) // 왼쪽
            {
                farthestGround.position = new Vector3(centralGround.position.x - groundWidth, farthestGround.position.y, farthestGround.position.z);
            }
            else // 중간이므로 판단보류
            {
                return;
            }
        }
    }
}
