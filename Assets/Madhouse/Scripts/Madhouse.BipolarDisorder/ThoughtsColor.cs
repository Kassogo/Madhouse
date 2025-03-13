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

        public void SetRandomColor()
        {
            if (_spriteRenderer == null) return;
            _spriteRenderer.color = Random.value > 0.5f ? Color.white : Color.black;
        }
    }
}
