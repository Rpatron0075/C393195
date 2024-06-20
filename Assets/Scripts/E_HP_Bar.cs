using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class E_HP_Bar : MonoBehaviour
{
    public GameObject HP_Bar_Value;
    private EnemyCombats combats;

    private float max_HP;
    private float current_HP;

    private void Start()
    {
        combats = gameObject.GetComponent<EnemyCombats>();
        max_HP = combats.enemyHealth;
        current_HP = max_HP;

        HP_Bar_Value.transform.localScale = new Vector3(current_HP / max_HP, HP_Bar_Value.transform.localScale.y, HP_Bar_Value.transform.localScale.z);
    }

    private void Update()
    {
        current_HP = combats.enemyHealth;
        HP_Bar_Value.transform.localScale = new Vector3(current_HP / max_HP, HP_Bar_Value.transform.localScale.y, HP_Bar_Value.transform.localScale.z);
    }
}
