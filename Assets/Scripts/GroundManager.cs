using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public Transform p_trans; // �÷��̾��� Transform
    public Transform[] grounds; // 3���� �׶��带 ���� �迭

    private Transform centralGround;
    private Transform farthestGround;

    private float groundWidth; // �׶����� ��

    void Start()
    {
        // �׶����� ���� ����մϴ�. ��� �׶��尡 ���� ���� �����ٰ� �����մϴ�.
        groundWidth = grounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // �÷��̾��� ��ġ�� �������� �߾� �׶��带 ã���ϴ�.
        centralGround = GetCentralGround();

        // ���� �ָ� �ִ� �׶��带 ã���ϴ�.
        farthestGround = GetFarthestGround(centralGround);

        // �÷��̾��� ��ġ�� ���� �׶��带 ���ġ�մϴ�.
        RepositionGrounds(centralGround, farthestGround);
    }

    Transform GetCentralGround()
    {
        // �÷��̾�� ���� ����� �׶��带 ã���ϴ�.
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
        // �߾� �׶��带 �����ϰ� ���� �ָ� �ִ� �׶��带 ã���ϴ�.
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

            float criterion = centralGround.position.x + ground.position.x / 2f;    // ���� �� �׶����� ��ġ ��ġ�� �Ǵ��� x ������

            if (p_trans.position.x - criterion > 0) // ������
            {
                farthestGround.position = new Vector3(centralGround.position.x + groundWidth, farthestGround.position.y, farthestGround.position.z);
            }
            else if (p_trans.position.x - criterion < 0) // ����
            {
                farthestGround.position = new Vector3(centralGround.position.x - groundWidth, farthestGround.position.y, farthestGround.position.z);
            }
            else // �߰��̹Ƿ� �Ǵܺ���
            {
                return;
            }
        }
    }
}
