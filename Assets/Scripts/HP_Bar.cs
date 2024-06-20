using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{
    public GameObject Owner;
    private PlayerCombats combats;
    protected float curHealth; //* ���� ü��
    protected float LastHealthLog;
    public float maxHealth; //* �ִ� ü��

    private float BarPrintTime = 2f;
    private float BarPrintTimer = 0f;
    private bool isChanged_HP = false;

    public GameObject BackGroundColor;
    public GameObject FillColor;
    private Image Back;
    private Image Fill;

    public float transparency = 1.0f; // ���� ���� ���� ������ ����

    public Slider HpBarSlider;

    private void Start()
    {
        combats = Owner.GetComponent<PlayerCombats>();
        SetHp();
        HpBarSlider.maxValue = maxHealth;
        HpBarSlider.value = curHealth;

        Back = BackGroundColor.GetComponent<Image>();
        Fill = FillColor.GetComponent<Image>();
    }

    private void Update()
    {
        CheckHp();
        SetAlphaValue();
    }

    private void SetHp() //*Hp����
    {
        maxHealth = combats.playerHealth;
        curHealth = maxHealth;
        LastHealthLog = curHealth;
    }

    public void CheckHp() //*HP ����
    {
        curHealth = combats.playerHealth; // Update current health from PlayerCombats

        if (LastHealthLog != curHealth)
        {
            isChanged_HP = true;
            LastHealthLog = curHealth;
            transparency = 1.0f;
            BarUpdated(curHealth);
        }
        else
        {
            DownGradeAlphaValue();
        }
    }

    private void BarUpdated(float targetValue)
    {
        HpBarSlider.value = targetValue;
        PrintTimer();
    }

    private void PrintTimer()
    {
        if (isChanged_HP)
        {
            BarPrintTimer += Time.deltaTime;
            if (BarPrintTimer >= BarPrintTime)
            {
                isChanged_HP = false;
                BarPrintTimer = 0f;
            }
        }
    }

    private void SetAlphaValue()
    {
        Color tempColor = Fill.color;
        tempColor.a = transparency;
        Fill.color = tempColor;

        tempColor = Back.color; 
        tempColor.a = transparency;
        Back.color = tempColor;
    }

    private void DownGradeAlphaValue()
    {
        if (transparency > 0)
        {
            transparency -= 0.01f;
        }
        if (transparency < 0)
        {
            transparency = 0;
        }
    }
}
