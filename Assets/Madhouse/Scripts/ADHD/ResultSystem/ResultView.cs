using UnityEngine;
using UnityEngine.UI;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка для показа результата игры.
    /// </summary>
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private GameObject _winWindow;
        [SerializeField] private GameObject _loseWindow;
        [SerializeField] private Image _blackVeil;

        private Color _colorVeil;

        /// <summary>
        /// Показ экрана победы.
        /// </summary>
        public void ShowWinWindow() => _winWindow.SetActive(true);

        /// <summary>
        /// Показ экрана проигрыша.
        /// </summary>
        public void ShowLoseWindow()
        {
            _loseWindow.SetActive(true);
            _blackVeil.gameObject.SetActive(false);
        }

        /// <summary>
        /// Изменение видимости чёрной пелены.
        /// </summary>
        /// <param name="visibility"></param>
        public void ChangeVisibilityVeil(float visibility)
        {
            _colorVeil = Color.black;
            _colorVeil.a = visibility;
            _blackVeil.color = _colorVeil;
        }
    }
}