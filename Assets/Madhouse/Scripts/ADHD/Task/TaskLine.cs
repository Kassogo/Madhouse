using UnityEngine;
using UnityEngine.UI;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Класс отображения линии задания.
    /// </summary>
    public class TaskLine : MonoBehaviour
    {
        [SerializeField] private Image _interactionSprite;
        [SerializeField] private Image _objectSprite;

        public void SetPictures(Sprite interaction = null, Sprite shape = null)
        {
            _interactionSprite.gameObject.SetActive(interaction != null);
            _objectSprite.gameObject.SetActive(shape != null);
            _objectSprite.color = Color.white;

            if (_interactionSprite != null)
                _interactionSprite.sprite = interaction;
            if (_objectSprite != null)
                _objectSprite.sprite = shape;
        }

        public void SetColor(Color color) => _objectSprite.color = color;
    }
}
