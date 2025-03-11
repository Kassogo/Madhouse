using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    public class ThoughtsCollision : MonoBehaviour
    {
        [SerializeField] private ColorController _colorController;
        private int _score = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.StartsWith("ThoughtPrefab"))
            {
                SpriteRenderer thoughtRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                if (thoughtRenderer == null) return;

                SpriteRenderer playerRenderer = _colorController.GetComponent<SpriteRenderer>();

                // Логика изменения счета
                if (playerRenderer.color == thoughtRenderer.color)
                {
                    _score += 1;
                }
                else
                {
                    _score -= 1;
                }

                // Удаляем шарик после столкновения
                Destroy(collision.gameObject);

                // Очищаем консоль перед выводом нового значения
                Debug.ClearDeveloperConsole();
                Debug.Log($"Score: {_score}");
            }
        }
    }
}