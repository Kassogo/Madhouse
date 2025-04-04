using System;
using System.Collections;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контроллер для фигур.
    /// </summary>
    public class ShapeController : MonoBehaviour
    {
        /// <summary>
        /// Конец существования фигуры.
        /// </summary>
        public event Action<ShapeController> OnEnd = delegate { };

        [SerializeField] private ShapeInteraction _shapeInteraction;
        [SerializeField] private float _minTimeOverTask = 8;
        [SerializeField] private float _maxTimeOverTask = 15;
        [SerializeField] private ShapeView _shapeView;

        private ShapeTypes _shapeType;
        private ShapeColors _color;
        private InteractionEndTypes _endtype;

        /// <summary>
        /// Тип фигуры.
        /// </summary>
        public ShapeTypes Type => _shapeType;

        /// <summary>
        /// Цвет.
        /// </summary>
        public ShapeColors ColorType => _color;

        /// <summary>
        /// Тип взаимодействия игрока и этой фигуры.
        /// </summary>
        public InteractionEndTypes EndType => _endtype;

        /// <summary>
        /// Инициализация фигуры.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="color"></param>
        public void Init(ShapeTypes shape, ShapeColors color)
        {
            _shapeType = shape;
            _shapeInteraction.OnInteractEnd += HaveEndInteraction;
            _color = color;

            _shapeView.ShowType(_shapeType, _color);
        }

        private void HaveEndInteraction(InteractionEndTypes interaction)
        {
            _shapeInteraction.OnInteractEnd -= HaveEndInteraction;
            _endtype = interaction;
            OnEnd.Invoke(this);
        }

        private IEnumerator OverUsed()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minTimeOverTask, _maxTimeOverTask));
            _endtype = InteractionEndTypes.None;
            OnEnd.Invoke(this);
        }
    }
}
