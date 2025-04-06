using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контроллер для высчитывания результата игры.
    /// </summary>
    public class ResultController : MonoBehaviour
    {
        [SerializeField] private ScoreController _scoreController;
        [SerializeField] private ResultView _resultView;
        [SerializeField] private ResultModel _resultModel;

        private void Awake()
        {
            _scoreController.OnChangeScore += CheckScore;
        }

        private void OnDestroy()
        {
            _scoreController.OnChangeScore -= CheckScore;
        }

        private void CheckScore(int score)
        {
            if (score >= _resultModel.WinScore)
                _resultView.ShowWinWindow();
            else if (score <= _resultModel.LoseScore)
                _resultView.ShowLoseWindow();

            CalculatingProximityToLoss(score);
        }

        private void CalculatingProximityToLoss(int score)
        {
            if (score >= 0)
            {
                _resultView.ChangeVisibilityVeil(0);
                return;
            }

            _resultView.ChangeVisibilityVeil((float)score / _resultModel.LoseScore);
        }
    }
}
