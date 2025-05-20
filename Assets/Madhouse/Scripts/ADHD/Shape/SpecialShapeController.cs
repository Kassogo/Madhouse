using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контроллер для специальных фигур.
    /// </summary>
    public class SpecialShapeController : MonoBehaviour
    {
        /// <summary>
        /// Событие конца итерации с игроком фигурой.
        /// </summary>
        public event Action<InteractionEndTypes, SpecialShapeTypes> OnEndInteraction = delegate { };

        [SerializeField] private ShapeInteraction _shapeInteraction;
        [SerializeField] private ShapeMove _shapeMove;
        [SerializeField] private SpecialShapeView _shapeView;
        [SerializeField] private ShapeSetting _setting;

        private SpecialShapeTypes _type;

        /// <summary>
        /// Тип специальной фигуры.
        /// </summary>
        public SpecialShapeTypes Type => _type;

        /// <summary>
        /// Инициализация специальной фигуры.
        /// </summary>
        /// <param name="shape"></param>
        public void Init(SpecialShapeTypes shape)
        {
            _type = shape;
            _shapeInteraction.OnInteractEnd += EndInteraction;

            _shapeView.ShowType(shape);
        }

        private void Awake()
        {
            _shapeInteraction.Init(_setting);
            _shapeMove.Init(_setting, _shapeInteraction);
        }

        private void EndInteraction(InteractionEndTypes interactionEndType)
        {
            OnEndInteraction.Invoke(interactionEndType, _type);
        }

        private void OnDisable()
        {
            _shapeInteraction.OnInteractEnd -= EndInteraction;
        }
    }
}
