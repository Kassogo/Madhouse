using Madhouse.AnxietyDisorder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource _voices;

    [SerializeField] AudioSource _breathBlade;
    [SerializeField] AudioSource _hit;

    [SerializeField] AudioSource _buttonClick;

    [SerializeField] AudioSource _catPurr;
    [SerializeField] AudioSource _womanBreath;
    [SerializeField] AudioSource _confidenceBoost;
    [SerializeField] AudioSource _mindOrderBoost;
    [SerializeField] AudioSource _inspiration;

    [SerializeField] AudioSource _smallEnemyHit1;
    [SerializeField] AudioSource _smallEnemyHit2;
    [SerializeField] AudioSource _smallEnemyHit3;
    [SerializeField] AudioSource _smallEnemyHit4;
    [SerializeField] AudioSource _smallEnemyHit5;
    [SerializeField] AudioSource _smallEnemyHit6;

    [SerializeField] AudioSource _enemyDie1;
    [SerializeField] AudioSource _enemyDie2;
    [SerializeField] AudioSource _enemyDie3;
    [SerializeField] AudioSource _enemyDie4;

    [SerializeField] AudioSource _weakEnemyDie;

    [SerializeField] AudioSource _coin1;
    [SerializeField] AudioSource _coin2;
    [SerializeField] AudioSource _coin3;
    [SerializeField] AudioSource _coin4;
    [SerializeField] AudioSource _coin5;

    [SerializeField] AudioSource _letter;

    [SerializeField] AudioSource _brainHit;

    private float _maxVoicesVolume;
    private float _currentVoicesVolume;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _maxVoicesVolume = 0.2f;
        _currentVoicesVolume = 0f;
    }

    private void Update()
    {
        _currentVoicesVolume = _maxVoicesVolume - (_maxVoicesVolume * (HealthBar.instance._currentHP / HealthBar.instance._maxHP));
        _voices.volume = _currentVoicesVolume;
    }

    public void BreathBlade()
    {
        _breathBlade.Play();
    }
    public void Hit()
    {
        _hit.Play();
    }

    public void ButtonClick()
    {
        _buttonClick.Play();
    }

    public void CatPurr()
    {
        _catPurr.Play();
    }
    public void WomanBreath()
    {
        _womanBreath.Play();
    }
    public void ConfidenceBoost()
    {
        _confidenceBoost.Play();
    }
    public void MindOrderBoost()
    {
        _mindOrderBoost.Play();
    }
    public void Inspiration()
    {
        _inspiration.Play();
    }

    public void SmallEnemyHit()
    {
            int randomSound = Random.Range(0, 6);
            switch (randomSound)
            {
                case 0: _smallEnemyHit1.Play(); return;
                case 1: _smallEnemyHit2.Play(); return;
                case 2: _smallEnemyHit3.Play(); return;
                case 3: _smallEnemyHit4.Play(); return;
                case 4: _smallEnemyHit5.Play(); return;
                case 5: _smallEnemyHit6.Play(); return;
            }
    }

    public void EnemyDie()
    {
        if (!HealthBar.instance._lose)
        {
            int randomSound = Random.Range(0, 4);
            switch (randomSound)
            {
                case 0: _enemyDie1.Play(); return;
                case 1: _enemyDie2.Play(); return;
                case 2: _enemyDie3.Play(); return;
                case 3: _enemyDie4.Play(); return;
            }
        }
    }

    public void WeakEnemyDie()
    {
        _weakEnemyDie.Play();
    }

    public void Coin(int index)
    {
        switch (index)
        {
            case 1: _coin1.Play(); return;
            case 2: _coin2.Play(); return;
            case 3: _coin3.Play(); return;
            case 4: _coin4.Play(); return;
            case 5: _coin5.Play(); return;
        }
    }

    public void Letter()
    {
        _letter.Play();
    }
    public void StopLetter()
    {
        _letter.Stop();
    }

    public void BrainHit()
    {
        _brainHit.Play();
    }
}
