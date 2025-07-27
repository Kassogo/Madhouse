using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

//In this script, the value of brain health is set and changed
namespace Madhouse.AnxietyDisorder
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] GameObject _particleSystemForceFieldGO;
        [SerializeField] GameObject _particleSystemGO;
        [SerializeField] GameObject _camera;
        [SerializeField] GameObject _panel;

        [SerializeField] private GameObject _brainBodyDamageVisual;

        public bool gameProcess;
        public bool _lose;
        public float _maxVignette;

        public static HealthBar instance;

        public float _maxHP;
        public float _currentHP;

        private bool hpRecovery;

        private ParticleSystemForceField _circleOfParticles;
        private float _maxScale;
        private float _currentScale;

        private PostProcessVolume _volume;
        private Vignette _vignette;
        private float _currentVignette;

        private void Awake()
        {
            instance = this;
            gameProcess = true;
        }

        private void Start()
        {
            _lose = false;
            hpRecovery = false;

            _maxHP = 100f;
            _currentHP = _maxHP;

            _circleOfParticles = _particleSystemForceFieldGO.GetComponent<ParticleSystemForceField>();
            _maxScale = 0.54f;
            _currentScale = _maxScale;

            _volume = _camera.GetComponent<PostProcessVolume>();
            _volume.profile.TryGetSettings(out _vignette);
            _maxVignette = 0.4f;
            _currentVignette = _maxVignette;

            _updateHealthBar();
        }

        //taking a life
        public void Damage(float damagePoint)
        {
            if(_currentHP > 0)
            {
                if (hpRecovery == false)
                {
                    _currentHP -= damagePoint * Bonuses.instance.confidenceScale;
                    SoundManager.instance.BrainHit();
                    StartCoroutine(BrainBodyDamage());
                }
            }
            else if (_lose == false)
            {
                _lose = true;
                _panel.SetActive(true);
                Panel.instance.StartFadeIn();
            }
            _updateHealthBar();
        }

        //updating healthbar
        public void _updateHealthBar()
        {
            if (_currentHP > 100)
            {
                _currentHP = 100;
            }

            if(_currentScale > 0)
            {
                _currentScale = _maxScale * (_currentHP / _maxHP);
            }
            _circleOfParticles.startRange = _currentScale;

            _currentVignette = _maxVignette * (_currentHP / _maxHP);
            _vignette.intensity.value = _maxVignette - _currentVignette;
        }

        public void FullHP()
        {
            StartCoroutine(StartFullHP());
        }
        IEnumerator StartFullHP()
        {
            hpRecovery = true;
            while (_currentHP < _maxHP)
            {
                _currentHP += 5;
                _updateHealthBar();
                yield return new WaitForSeconds(0.1f);
            }
            hpRecovery = false;
        }

        private IEnumerator BrainBodyDamage()
        {
            _brainBodyDamageVisual.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _brainBodyDamageVisual.SetActive(false);
        }
    }
}