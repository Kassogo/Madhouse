using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public float _timeUnit;

    private float _timer;
    private float _cntSeconds;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _timeUnit = 5;

        _timer = 1f;
        _cntSeconds = 0f;
    }

    private void Update()
    {
        if (_timer <= 0f)
        {
            _timer = 1f;
            _cntSeconds += 1f;

            if(_cntSeconds >= 30f)
            {
                if (_timeUnit > 1)
                {
                    _timeUnit--;
                    _cntSeconds = 0f;
                }
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
