using UnityEngine;
using UnityEngine.SceneManagement;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контроллер для меню паузы.
    /// </summary>
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private PauseMenuView _menuView;

        private bool _isShowMenu = false;
        private bool _isEndGame = false;

        private float _timeScaleNow;

        /// <summary>
        /// Показ меню паузы для конца игры.
        /// </summary>
        public void ShowPauseMenuForEndGame()
        {
            _isEndGame = true;
            ActivatePauseMenu();
        }

        private void Update()
        {
            if (!_isEndGame)
                TrackButton();
        }

        private void Awake()
        {
            Time.timeScale = 1;
            _menuView.onClickButtonGoToMainMenu += GoToMainMenu;
            _menuView.onClickButtonRestart += RestartLevel;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
            _menuView.onClickButtonGoToMainMenu -= GoToMainMenu;
            _menuView.onClickButtonRestart -= RestartLevel;
        }

        private void TrackButton()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) SetStatysPauseMenu();
        }

        private void SetStatysPauseMenu()
        {
            _isShowMenu = !_isShowMenu;

            if (_isShowMenu)
                ActivatePauseMenu();
            else
                DeactivatePauseMenu();
        }

        private void ActivatePauseMenu()
        {
            _menuView.ShowMenu();
            _timeScaleNow = Time.timeScale;
            Time.timeScale = 0;
        }

        private void DeactivatePauseMenu()
        {
            _menuView.HideMenu();
            Time.timeScale = _timeScaleNow;
        }

        private void GoToMainMenu() => SceneManager.LoadScene(0);
        private void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
