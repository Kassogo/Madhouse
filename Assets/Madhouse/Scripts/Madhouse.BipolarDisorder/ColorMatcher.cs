using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    public class ColorMatcher : MonoBehaviour
    {
        private SpriteRenderer _playerRenderer; // Спрайт рендерер игрока
        private ColorController _playerColorController; // Ссылка на ColorController игрока

        private void Awake()
        {
            _playerRenderer = GetComponent<SpriteRenderer>();
            _playerColorController = GetComponent<ColorController>(); // Получаем ссылку на ColorController
        }

        public bool IsColorMatch(SpriteRenderer thoughtRenderer)
        {
            // Получаем скрипт ThoughtsColor с объекта мысли
            ThoughtsColor thoughtColorScript = thoughtRenderer.GetComponent<ThoughtsColor>();

            if (thoughtColorScript == null)
            {
                Debug.LogError("ColorMatcher: ThoughtsColor script not found on thought object!");
                return false;
            }

            // Получаем "цвет" мысли из её скрипта ThoughtsColor
            Color thoughtActualColor = thoughtColorScript.CurrentThoughtColor;

            // Получаем текущий "цвет" игрока из его ColorController (белый или черный)
            Color playerActualColor = _playerColorController.CurrentPlayerColor; // Мы добавим это свойство в ColorController

            return playerActualColor == thoughtActualColor;
        }
    }
}