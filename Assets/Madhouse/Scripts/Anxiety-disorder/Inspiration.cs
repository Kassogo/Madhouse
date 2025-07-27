using Madhouse.AnxietyDisorder;
using System.Collections;
using TMPro;
using UnityEngine;

public class Inspiration : MonoBehaviour
{
    public static Inspiration instance;

    [SerializeField] GameObject _inspirationButton;
    [SerializeField] TextMeshProUGUI _inspirationCost;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _inspirationButton.SetActive(false);
        StartCoroutine(ReloadTimer());
    }

    public void ReloadInspirationTimer()
    {
        StopAllCoroutines();
        StartCoroutine(ReloadTimer());
    }

    IEnumerator ReloadTimer()
    {
        _inspirationButton.SetActive(false);
        Bonuses.instance.inspirationOn = false;
        int timer = Random.Range(30, 61);
        yield return new WaitForSeconds(timer);

        if (HealthBar.instance._currentHP > 60)
        {
            _inspirationCost.text = "60";
        }
        else if (HealthBar.instance._currentHP >= 30 && HealthBar.instance._currentHP <= 60)
        {
            _inspirationCost.text = "50";
        }
        else
        {
            _inspirationCost.text = "40";
        }

        if (Timer.instance.endGame == true)
        {
            StopAllCoroutines();
        }
        else
        {
            _inspirationButton.SetActive(true);
            Bonuses.instance.inspirationOn = true;
            yield return new WaitForSeconds(10);
            ReloadInspirationTimer();
        }
    }
}
