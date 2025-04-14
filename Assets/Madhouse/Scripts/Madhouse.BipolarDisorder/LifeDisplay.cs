using UnityEngine;
using TMPro;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Displays the player's lives on the screen.
    /// </summary>
    public class LifeDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _livesText;

        private void Start()
        {
            if (LifeManager.Instance == null)
            {
                Debug.LogError("LifeManager.Instance is null! LifeDisplay не сможет подписаться на события.");
                return;
            }

            LifeManager.Instance.OnLivesChanged += UpdateLivesText;
            UpdateLivesText(LifeManager.Instance.Lives);
        }

        private void OnDestroy()
        {
            if (LifeManager.Instance != null)
            {
                LifeManager.Instance.OnLivesChanged -= UpdateLivesText;
            }
        }

        private void UpdateLivesText(int lives)
        {
            if (_livesText != null)
            {
                _livesText.text = $"Lives: {lives}";
            }
        }
    }
}
