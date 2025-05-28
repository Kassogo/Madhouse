using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

//In this script, the value of brain health is set and changed
namespace Madhouse.AnxietyDisorder
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] GameObject _particleSystemForceFieldGO;
        [SerializeField] GameObject _particleSystemGO;
        [SerializeField] GameObject _camera;

        public static HealthBar instance;

        private float _maxHP;
        private float _currentHP;

        private ParticleSystemForceField _circleOfParticles;
        private float _maxScale;
        private float _currentScale;

        private PostProcessVolume _volume;
        private Vignette _vignette;
        private float _maxVignette;
        private float _currentVignette;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
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
                _currentHP -= damagePoint * Bonuses.instance.confidenceScale;
            }
            else
            {
                _currentHP = 0;
            }
            _updateHealthBar();
        }

        //updating healthbar
        private void _updateHealthBar()
        {
            if(_currentScale > 0)
            {
                _currentScale = _maxScale * (_currentHP / _maxHP);
            }
            _circleOfParticles.startRange = _currentScale;

            _currentVignette = _maxVignette * (_currentHP / _maxHP);
            _vignette.intensity.value = _maxVignette - _currentVignette;
        }
    }
}