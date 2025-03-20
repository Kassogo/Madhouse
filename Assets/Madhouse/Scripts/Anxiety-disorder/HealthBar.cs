using UnityEngine;
using UnityEngine.UI;


namespace Madhouse.AnxietyDisorder
{
    public class HealthBar : MonoBehaviour
    {
        public static HealthBar instance;

        private float _maxHP;
        private float _currentHP;
        [SerializeField] private Image _healthBar;

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            _maxHP = 100f;
            _currentHP = _maxHP;
        }
        public void Damage(float damagePoint)
        {
            _currentHP -= damagePoint;
            _updateHealthBar();
        }
        private void _updateHealthBar()
        {
            _healthBar.fillAmount = _currentHP / _maxHP;
        }
    }
}