using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Настройки для системы заданий.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TaskSetting), menuName = "Madhouse/ADHD/" + nameof(TaskSetting))]
    public class TaskSetting : ScriptableObject
    {
        [SerializeField] private float _minTimeChangeTask = 8;
        [SerializeField] private float _maxTimeChangeTask = 15;
        [Space]
        [SerializeField] private float _minTimeSpecialTask = 20;
        [SerializeField] private float _maxTimeSpecialTask = 30;
        [Space]
        [Header("Лопнуть, Забрать, Зажать, Потрясти")]
        [SerializeField] private Sprite[] _spritesInteraction;
        [SerializeField] private Sprite _blob;
        [SerializeField] private ShapesData _spritesForm;
        [SerializeField] private ColorsData _colors;

        /// <summary>
        /// Минимальное время изменения заданий.
        /// </summary>
        public float MinTimeChangeTask => _minTimeChangeTask;

        /// <summary>
        /// Максимальное время изменения заданий.
        /// </summary>
        public float MaxTimeChangeTask => _maxTimeChangeTask;

        /// <summary>
        /// Минимальное время изменения особых заданий.
        /// </summary>
        public float MinTimeChangeSpecialTask => _minTimeSpecialTask;

        /// <summary>
        /// Максимальное время изменения особых заданий.
        /// </summary>
        public float MaxTimeChangeSpecialTask => _maxTimeSpecialTask;

        /// <summary>
        /// Спрайт кляксы.
        /// </summary>
        public Sprite BlobSprite => _blob;

        /// <summary>
        /// Получение цвета по типу.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Color GetColor(ShapeColors color)
        {
            for(int i = 0; i < _colors.Colors.Count; i++)
            {
                if (_colors.Colors[i].ColorType == color)
                    return _colors.Colors[i].ColorValue;
            }

            Debug.LogError("Не найден необходимый цвет!");
            return Color.white;
        }

        /// <summary>
        /// Получение спрайта фигуры.
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public Sprite GetForm(ShapeTypes shape)
        {
            for (int i = 0; i < _spritesForm.Shapes.Count; i++)
            {
                if (_spritesForm.Shapes[i].Type == shape)
                    return _spritesForm.Shapes[i].Picture;
            }

            Debug.LogError("Не найден необходимая фигура!");
            return null;
        }

        /// <summary>
        /// Получение спрайта итерации с объектом.
        /// </summary>
        /// <param name="interactionType"></param>
        /// <returns></returns>
        public Sprite GetSpriteInteraction(InteractionEndTypes interactionType)
        {
            return _spritesInteraction[(int)interactionType];
        }
    }
}
