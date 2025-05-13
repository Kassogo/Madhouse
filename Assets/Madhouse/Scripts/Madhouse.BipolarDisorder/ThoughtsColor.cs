using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Assigns a random color to the thought.
    /// </summary>
    public class ThoughtsColor : MonoBehaviour
    {
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetColor(Color color)
        {
            if (_spriteRenderer == null) return;
            _spriteRenderer.color = color;
        }
    }
}
