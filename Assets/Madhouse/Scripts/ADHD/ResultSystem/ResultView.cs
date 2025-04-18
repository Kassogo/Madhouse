using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка для показа результата игры.
    /// </summary>
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textResult;
        [SerializeField] private string _winText;
        [SerializeField] private string _loseText;
        [SerializeField] private Image _blackVeil;

        private Color _colorVeil;

        /// <summary>
        /// Показ экрана победы.
        /// </summary>
        public void ShowWinWindow()
        {
            _textResult.gameObject.SetActive(true);
            _textResult.text = _winText;
        }

        /// <summary>
        /// Показ экрана проигрыша.
        /// </summary>
        public void ShowLoseWindow()
        {
            _textResult.gameObject.SetActive(true);
            _blackVeil.gameObject.SetActive(false);
            _textResult.text = _loseText;
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