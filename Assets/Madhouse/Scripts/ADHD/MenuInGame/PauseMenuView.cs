using System;
using UnityEngine;
using UnityEngine.UI;

namespace Madhouse.ADHD
{
    public class PauseMenuView : MonoBehaviour
    {
        public event Action onClickButtonGoToMainMenu = delegate { };
        public event Action onClickButtonRestart = delegate { };

        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private Button _buttonGoToMainMenu;
        [SerializeField] private Button _buttonRestartLevel;

        public void ShowMenu() => _panelMenu.SetActive(true);

        public void HideMenu() => _panelMenu.SetActive(false);

        private void Awake()
        {
            _buttonGoToMainMenu.onClick.AddListener(ClickButtonGoToMainMenu);
            _buttonRestartLevel.onClick.AddListener(ClickButtonRestart);
        }

        private void OnDestroy()
        {
            _buttonGoToMainMenu.onClick.RemoveListener(ClickButtonGoToMainMenu);
            _buttonRestartLevel.onClick.RemoveListener(ClickButtonRestart);
        }

        private void ClickButtonGoToMainMenu() => onClickButtonGoToMainMenu.Invoke();
        private void ClickButtonRestart() => onClickButtonRestart.Invoke();
    }
}
