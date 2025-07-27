using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject _circle;
    [SerializeField] GameObject _printsGO;
    [SerializeField] Image _prints;
    private float _timer;

    private void OnEnable()
    {
        _timer = 10;
        StartCoroutine(Circle());
        StartCoroutine(Prints());
    }
    private void OnDisable()
    {
        Bonuses.instance.shieldOn = false;
        this.StopAllCoroutines();
    }

    private void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator Circle()
    {
        float circleSize = 0;

        _circle.transform.localScale = new Vector3(circleSize, circleSize, circleSize);
        _printsGO.transform.rotation =new Quaternion(0, 0, 0, 0);


        while(circleSize < 1)
        {
            circleSize += 0.03f;
            _circle.transform.localScale = new Vector3(circleSize, circleSize, circleSize);
            yield return null;
        }
    }

    IEnumerator Prints()
    {
        float printsVisible = 0;
        _prints.fillAmount = printsVisible;

        float timer = 1;
        while (timer > 0)
        {
            timer -= 0.1f;
            yield return null;
        }

        while (printsVisible < 1)
        {
            printsVisible += 0.004f;
            _prints.fillAmount = printsVisible;
            yield return null;
        }
    }
}