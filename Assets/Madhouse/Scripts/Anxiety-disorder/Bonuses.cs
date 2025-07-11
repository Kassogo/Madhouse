using Madhouse.AnxietyDisorder;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Bonuses : MonoBehaviour
{
    public static Bonuses instance;

    [SerializeField] GameObject shieldGO;
    [SerializeField] GameObject mindBreathGO;
    [SerializeField] GameObject mainCamera;

    [SerializeField] Image shieldBar;
    [SerializeField] Image mindBreathBar;
    [SerializeField] Image confidenceBar;
    [SerializeField] Image mindOrderBar;

    [SerializeField] TextMeshProUGUI confidenceText;
    [SerializeField] TextMeshProUGUI mindOrderText;

    public bool shieldOn;
    public bool mindBreathOn;
    public float confidenceScale;
    public float mentalOrderScale;

    public bool inspirationOn;

    private float _maxConfidenceBarValue;
    private float _maxMindOrderBarValue;
    private float _currentConfidenceBarValue;
    private float _currentMindOrderBarValue;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        shieldGO.SetActive(false);

        mainCamera.GetComponent<SC_CursorTrail>().enabled = false;

        shieldOn = false;
        mindBreathOn = false;
        confidenceScale = 1;
        mentalOrderScale = 1;

        shieldBar.fillAmount = 0;
        mindBreathBar.fillAmount = 0;

        _maxConfidenceBarValue = 100;
        _maxMindOrderBarValue = 100;
        _currentConfidenceBarValue = 0;
        _currentMindOrderBarValue = 0;

        inspirationOn = false;

        UpdateConfidenceBarAndText();
        UpdateMindOrderBarAndText();
    }

    private void Update()
    {
        if (!LevelManager.instance.onPause)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ShieldActivate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                MindBreathActivate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ConfidenceBoost();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                MindOrderBoost();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InspirationActivate();
            }
        }
    }

    public void ShieldActivate()
    {
        if (!Panel.instance.TheEnd)
        {
            SoundManager.instance.ButtonClick();
            if (shieldOn == false && Money.instance._cntMoney >= 30)
            {
                SoundManager.instance.CatPurr();

                Money.instance.SubMoney(30);

                shieldGO.SetActive(true);
                shieldOn = true;
                StartCoroutine(ShieldBar());
            }
        }
    }

    public void MindBreathActivate()
    {
        if (!Panel.instance.TheEnd)
        {
            SoundManager.instance.ButtonClick();
            if (mindBreathOn == false && Money.instance._cntMoney >= 35)
            {
                SoundManager.instance.WomanBreath();

                Money.instance.SubMoney(35);

                mindBreathGO.SetActive(true);
                mindBreathOn = true;
                StartCoroutine(MindBreathBar());
            }
        }
    }

    public void ConfidenceBoost()
    {
        if (!Panel.instance.TheEnd)
        {
            SoundManager.instance.ButtonClick();
            if (confidenceScale > 0.5 && Money.instance._cntMoney >= 40 && Timer.instance.CanBuyBoost() == true)
            {
                SoundManager.instance.ConfidenceBoost();

                Money.instance.SubMoney(40);

                confidenceScale -= 0.05f;
                _currentConfidenceBarValue += 10;
                UpdateConfidenceBarAndText();

                if (_currentConfidenceBarValue >= 100 && _currentMindOrderBarValue >= 100)
                {
                    Timer.instance.Finall();
                }
            }
        }
    }

    public void MindOrderBoost()
    {
        if (!Panel.instance.TheEnd)
        {
            SoundManager.instance.ButtonClick();
            if (mentalOrderScale > 0.5 && Money.instance._cntMoney >= 40 && Timer.instance.CanBuyBoost() == true)
            {
                SoundManager.instance.MindOrderBoost();

                Money.instance.SubMoney(40);

                mentalOrderScale -= 0.05f;
                _currentMindOrderBarValue += 10;
                UpdateMindOrderBarAndText();

                if (_currentConfidenceBarValue >= 100 && _currentMindOrderBarValue >= 100)
                {
                    Timer.instance.Finall();
                }
            }
        }
    }

    public void InspirationActivate()
    {
        if (!Panel.instance.TheEnd && inspirationOn)
        {
                SoundManager.instance.ButtonClick();

                if (HealthBar.instance._currentHP > 60 && Money.instance._cntMoney >= 60)
                {
                    Money.instance.SubMoney(60);

                    HealthBar.instance.FullHP();

                    Inspiration.instance.ReloadInspirationTimer();
                    SoundManager.instance.Inspiration();
                }
                else if (HealthBar.instance._currentHP >= 30 && HealthBar.instance._currentHP <= 60 && Money.instance._cntMoney >= 50)
                {
                    Money.instance.SubMoney(50);

                    HealthBar.instance.FullHP();

                    Inspiration.instance.ReloadInspirationTimer();
                    SoundManager.instance.Inspiration();
                }
                else if (HealthBar.instance._currentHP < 30 && Money.instance._cntMoney >= 40)
                {
                    Money.instance.SubMoney(40);

                    HealthBar.instance.FullHP();

                    Inspiration.instance.ReloadInspirationTimer();
                    SoundManager.instance.Inspiration();
                }
        }
    }

    IEnumerator ShieldBar()
    {
        float maxValue = 10f;
        float currentValue = maxValue;
        for (int i = 10; i >= 0; i--)
        {
            shieldBar.fillAmount = currentValue / maxValue;
            currentValue--;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator MindBreathBar()
    {
        float maxValue = 10f;
        float currentValue = maxValue;
        for (int i = 10; i >= 0; i--)
        {
            mindBreathBar.fillAmount = currentValue / maxValue;
            currentValue--;
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateConfidenceBarAndText()
    {
        confidenceBar.fillAmount = _currentConfidenceBarValue / _maxConfidenceBarValue;
        confidenceText.text = $"{_currentConfidenceBarValue}%";
    }

    private void UpdateMindOrderBarAndText()
    {
        mindOrderBar.fillAmount = _currentMindOrderBarValue / _maxMindOrderBarValue;
        mindOrderText.text = $"{_currentMindOrderBarValue}%";
    }
}