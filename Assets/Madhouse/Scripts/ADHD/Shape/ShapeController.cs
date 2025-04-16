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
        [SerializeField] private ShapeMove _shapeMove;
        [SerializeField] private ShapeSetting _setting;
        [SerializeField] private ShapeView _shapeView;

        private ShapeTypes _shapeType;
        private ShapeColors _color;
        private InteractionEndTypes _endtype;
        private Coroutine _coroutineDeath;

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
            _coroutineDeath = StartCoroutine(OverUsed());
        }

        private void Awake()
        {
            _shapeInteraction.Init(_setting);
            _shapeMove.Init(_setting, _shapeInteraction);
        }

        private void OnDisable()
        {
            if (_coroutineDeath != null)
                StopCoroutine(_coroutineDeath);
        }

        private void HaveEndInteraction(InteractionEndTypes interaction)
        {
            StopCoroutine(_coroutineDeath);
            _shapeInteraction.OnInteractEnd -= HaveEndInteraction;
            _endtype = interaction;
            OnEnd.Invoke(this);
        }

        private IEnumerator OverUsed()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_setting.MinTimeOverShape, _setting.MaxTimeOverShape));
            _endtype = InteractionEndTypes.None;
            OnEnd.Invoke(this);
        }
    }
}
