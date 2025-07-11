using Madhouse.AnxietyDisorder;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    [SerializeField] private GameObject _weakEnemy;

    [SerializeField] private GameObject _finallTextGO;
    private TextMeshProUGUI _textMeshPro;
    private string _text1;
    private string _text2;
    private string _text3;

    public bool restOn;

    public float _timeUnit;

    private float _timer;
    private float _cntSeconds;

    public bool finallPartPreparation;
    public bool finallPart;
    public bool endGame;

    public bool onSuperPower;

    public int cntEnemiesBorn;
    public int cntEnemiesDie;

    private void Awake()
    {
        Time.timeScale = 1;
        instance = this;
        _textMeshPro = _finallTextGO.GetComponent<TextMeshProUGUI>();
        _text1 = "Всё внутри стало яснее.\r\nСобранность. Уверенность.";
        _text2 = "Но даже теперь мысли могут вернуться.\r\nНеугомонные. Навязчивые. Все разом.";
        _text3 = "Это нормально.\r\nГлавное — ты знаешь, что с ними делать.";
    }

    private void Start()
    {
        restOn = false;

        _weakEnemy.SetActive(false);

        _timeUnit = 3;

        _timer = 1f;
        _cntSeconds = 0f;

        finallPartPreparation = false;
        finallPart = false;
        endGame = false;

        onSuperPower = false;

        cntEnemiesBorn = 0;
        cntEnemiesDie = 0;

        _finallTextGO.SetActive(false);
    }

    private void Update()
    {
        if (_timer <= 0f)
        {
            _timer = 1f;
            _cntSeconds += 1f;

            if (_cntSeconds >= 30f && !finallPartPreparation && !finallPart && !endGame)
            {
                if (!restOn)
                {
                    restOn = true;
                }

                if (_cntSeconds >= 35 && !finallPartPreparation && !finallPart && !endGame)
                {
                    if (_timeUnit > 1.5)
                    {
                        _timeUnit -= 0.5f;
                    }
                    _cntSeconds = 0f;
                    restOn = false;
                }
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    public bool CanBuyBoost()
    {
        if (finallPartPreparation == false && finallPart == false && endGame == false && !finallPart)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Finall()
    {
        restOn = false;
        StartCoroutine(StartFinall());
    }

    IEnumerator StartFinall()
    {
        float charDelay = 0.05f;
        float textdelay = 3f;

        finallPartPreparation = true;
        yield return new WaitForSeconds(7f);

        _finallTextGO.SetActive(true);
        _textMeshPro.text = "";
        for (int i = 0; i < _text1.Length; i++)
        {
            _textMeshPro.text += _text1[i];
            SoundManager.instance.Letter();
            yield return new WaitForSeconds(charDelay);
            SoundManager.instance.StopLetter();
        }
        yield return new WaitForSeconds(textdelay);

        _textMeshPro.text = "";
        for (int i = 0; i < _text2.Length; i++)
        {
            _textMeshPro.text += _text2[i];
            SoundManager.instance.Letter();
            yield return new WaitForSeconds(charDelay);
            SoundManager.instance.StopLetter();
        }
        yield return new WaitForSeconds(textdelay);

        _textMeshPro.text = "";
        for (int i = 0; i < _text3.Length; i++)
        {
            _textMeshPro.text += _text3[i];
            SoundManager.instance.Letter();
            yield return new WaitForSeconds(charDelay);
            SoundManager.instance.StopLetter();
        }
        yield return new WaitForSeconds(textdelay);
        _finallTextGO.SetActive(false);
        finallPartPreparation = false;

        finallPart = true;
        onSuperPower = true;
        _timeUnit = 0.5f;
        cntEnemiesBorn = 0;
        cntEnemiesDie = 0;
        yield return new WaitForSeconds(60f);
        finallPart = false;

        while (cntEnemiesBorn < 100)
        {
            yield return null;
        }
    
        endGame = true;

        while (cntEnemiesDie != cntEnemiesBorn)
        {
            yield return null;
        }
        HealthBar.instance.FullHP();
        yield return new WaitForSeconds(5f);

        _weakEnemy.SetActive(true);
    }
}
