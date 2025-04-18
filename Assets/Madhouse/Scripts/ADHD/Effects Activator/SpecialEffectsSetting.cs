using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Настройки специальных эффектов.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SpecialEffectsSetting), menuName = "Madhouse/ADHD/" + nameof(SpecialEffectsSetting))]
    public class SpecialEffectsSetting : ScriptableObject
    {
        [SerializeField] private int _scoreChange = 5;
        [SerializeField] private float _timeEffect = 6;
        [SerializeField] private float _timeCoefficientChange = 0.2f;

        /// <summary>
        /// Изменение счёта при эффекте.
        /// </summary>
        public int ScoreChange => _scoreChange;

        /// <summary>
        /// Кол-во времени действия эффекта замедляющего время.
        /// </summary>
        public float TimeEffect => _timeEffect;

        /// <summary>
        /// Коэффициент изменения времени.
        /// </summary>
        public float TimeCoefficientChange => _timeCoefficientChange;
    }
}
