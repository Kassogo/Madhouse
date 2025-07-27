using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модель окончания игры.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ResultModel), menuName = "Madhouse/ADHD/"+ nameof(ResultModel))]
    public class ResultModel : ScriptableObject
    {
        [SerializeField] private int _winScore;
        [SerializeField] private int _loseScore;

        /// <summary>
        /// Кол-во очков для победы.
        /// </summary>
        public int WinScore => _winScore;

        /// <summary>
        /// Кол-во очков для проигрыша.
        /// </summary>
        public int LoseScore => _loseScore;
    }
}