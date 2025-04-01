using UnityEngine;
using UnityEngine.UI;

//In this script, the value of brain health is set and changed
namespace Madhouse.AnxietyDisorder
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        public static HealthBar instance;

        private float _maxHP;
        private float _currentHP;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _maxHP = 100f;
            _currentHP = _maxHP;
        }

        //taking a life
        public void Damage(float damagePoint)
        {
            _currentHP -= damagePoint;
            _updateHealthBar();
        }

        //updating healthbar
        private void _updateHealthBar()
        {
            _healthBar.fillAmount = _currentHP / _maxHP;
        }
    }
}