using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animeFunctionEvents : MonoBehaviour
{
    public PlayerController pc;

    void SetHitBoxActive()
    {
        pc.SetHitBoxActive();
    }

    void SetHitBoxNotActive()
    {
        pc.SetHitBoxNotActive();
    }
}
