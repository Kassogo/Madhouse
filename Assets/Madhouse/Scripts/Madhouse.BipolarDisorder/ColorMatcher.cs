using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Compares the player's color with the thought's color.
    /// </summary>
    public class ColorMatcher : MonoBehaviour
    {
        private SpriteRenderer _playerRenderer;

        private void Awake()
        {
            _playerRenderer = GetComponent<SpriteRenderer>();
        }

        public bool IsColorMatch(SpriteRenderer thoughtRenderer) => _playerRenderer.color == thoughtRenderer.color;
    }
}