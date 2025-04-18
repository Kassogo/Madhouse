using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Настройка получаемых и отнимаемых очков.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ScoreSetting), menuName = "Madhouse/ADHD/" + nameof(ScoreSetting))]
    public class ScoreSetting : ScriptableObject
    {
        [SerializeField] private int _addScoreForFirstTask = 2;
        [SerializeField] private int _addScoreForSecondTask = 1;
        [SerializeField] private int _removeScoreForWrongTask = 2;
        [SerializeField] private int _removeScoreAfterChangeTask = 1;

        /// <summary>
        /// Получаемые очки за выполнения первого задания.
        /// </summary>
        public int AddScoreForCompletFirstTask => _addScoreForFirstTask;

        /// <summary>
        /// Получаемые очки за выполнения второго задания.
        /// </summary>
        public int AddScoreForCompletSecondTask => _addScoreForSecondTask;

        /// <summary>
        /// Отнимаемые очки за выполнения первого задания.
        /// </summary>
        public int RemoveScoreForCompletWrongTask => _removeScoreForWrongTask;

        /// <summary>
        /// Получаемые очки за невыполнения задания.
        /// </summary>
        public int RemoveScoreForAfterChangeTask => _removeScoreAfterChangeTask;
    }
}
