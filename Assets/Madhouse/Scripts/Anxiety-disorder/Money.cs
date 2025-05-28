using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static Money instance;

    [SerializeField] private TextMeshProUGUI _moneyText;

    public float _cntMoney;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _cntMoney = 0;
        UpdateMoneyText();
    }

    public void AddMoney(float coinValue)
    {
        StartCoroutine(DelayMoney(coinValue));
    }

    public void SubMoney(float subValue)
    {
        _cntMoney -= subValue;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        _moneyText.text = _cntMoney.ToString();
    }
    
    IEnumerator DelayMoney(float coinValue)
    {
        if (coinValue > 5)
        {
            for (float i = 1; i <= coinValue; i++)
            {
                _cntMoney++;
                UpdateMoneyText();
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            for (float i = 1; i <= coinValue; i++)
            {
                _cntMoney++;
                UpdateMoneyText();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
