using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Madhouse.ADHD
{
    public class ShapesDispatcher : MonoBehaviour
    {
        public event Action<ShapeTypes, ShapeColors, InteractionEndTypes> OnDestroyObject = delegate { }; 
        public event Action<SpecialShapeTypes, InteractionEndTypes> OnDestroySpecialObject = delegate { };

        [SerializeField] private ShapeController _shapePrefab;
        [SerializeField] private SpecialShapeController _specialShapePrefab;
        [SerializeField] private int _StartCountShape;
        [SerializeField] private int _countShape;

        private ShapesPool _shapesPool;
        private SpecialShapePool _specialShapePool;
        private float _cooldownCreated = 2f;
        private float _timerCreatedShape;
        private Vector3 _spawnPoint;

        /// <summary>
        /// Создание специальных фигур.
        /// </summary>
        /// <param name="specialShape"></param>
        public void CreateSpecialShape(SpecialShapeTypes specialShape)
        {
            _specialShapePool.ActivateSpecialShape(_spawnPoint, specialShape);
        }

        /// <summary>
        /// Удаление фигуры по типу.
        /// </summary>
        /// <param name="shapeColor"></param>
        public void DeleteShape(ShapeColors shapeColor)
        {
            _shapesPool.DeactivateByCondition((ShapeController shape) => shape.ColorType == shapeColor);
        }

        /// <summary>
        /// Удаление фигуры по типу.
        /// </summary>
        /// <param name="shape"></param>
        public void DeleteShape(ShapeTypes shapeType)
        {
            _shapesPool.DeactivateByCondition((ShapeController shape) => shape.Type == shapeType);
        }

        /// <summary>
        /// Удаление фигуры по типу.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="shapeColor"></param>
        public void DeleteShape(ShapeTypes shapeType, ShapeColors shapeColor)
        {
            _shapesPool.DeactivateByCondition((ShapeController shape) => shape.Type == shapeType && shape.ColorType == shapeColor);
        }

        private void Awake()
        {
            _spawnPoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
            _spawnPoint.x -= 1;
            _spawnPoint.z = 0;

            _shapesPool = new ShapesPool(_shapePrefab, _countShape);
            _shapesPool.OnUseShape += UseShape;
            _specialShapePool = new SpecialShapePool(_specialShapePrefab);
            _specialShapePool.OnUseSpecialObject += UseSpecialShape;

            for (int i = 0; i < _StartCountShape; i++)
                CreatedShape();
        }

        private void Update()
        {
            if (_timerCreatedShape > Time.time)
                return;

            if (_shapesPool.CountActivateShape < _countShape)
                CreatedShape();
        }

        private void OnDisable()
        {
            _specialShapePool.OnUseSpecialObject -= UseSpecialShape;
            _shapesPool.OnUseShape -= UseShape;
            _specialShapePool.Disable();
            _shapesPool.Disable();
        }

        private void CreatedShape()
        {
            _timerCreatedShape = Time.time + _cooldownCreated;

            ShapeController shape = _shapesPool.GetFromPool();
            shape.transform.position = _spawnPoint;
            shape.gameObject.SetActive(true);

            shape.Init((ShapeTypes)Random.Range(0, Enum.GetValues(typeof(ShapeTypes)).Length),
                (ShapeColors)Random.Range(0, Enum.GetValues(typeof(ShapeColors)).Length));
        }

        private void UseShape(ShapeTypes shapeType, ShapeColors shapeColor, InteractionEndTypes interactionEnd)
        {
            OnDestroyObject.Invoke(shapeType, shapeColor, interactionEnd);
        }

        private void UseSpecialShape(InteractionEndTypes interactionEndType, SpecialShapeTypes specialShapeType)
        {
            OnDestroySpecialObject.Invoke(specialShapeType, interactionEndType);
        }
    }
}
