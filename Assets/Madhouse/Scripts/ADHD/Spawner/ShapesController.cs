using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Madhouse.ADHD
{
    public class ShapesController : MonoBehaviour
    {
        public event Action<ShapeTypes, ShapeColors, InteractionEndTypes> OnDestroyObject = delegate { }; 
        public event Action<SpecialShapeTypes, InteractionEndTypes> OnDestroySpecialObject = delegate { };

        [SerializeField] private ShapeController _shapePrefab;
        [SerializeField] private SpecialShapeController _specialShapePrefab;
        [SerializeField] private int _StartCountShape;
        [SerializeField] private int _countShape;

        private List<ShapeController> _shapes;
        private SpecialShapeController _correctShape;
        private SpecialShapeController _wrongShape;
        private float _cooldownCreated = 2f;
        private float _timerCreatedShape;
        private Vector3 _spawnPoint;

        private bool _isNeedDeleteShape;
        private int _indexShapeForDelete;

        /// <summary>
        /// Создание специальных фигур.
        /// </summary>
        /// <param name="specialShape"></param>
        public void CreateSpecialShape(SpecialShapeTypes specialShape)
        {
            if(_correctShape != null && _wrongShape != null)
            {
                DestroySpecialShape();
            }
            _correctShape = Instantiate(_specialShapePrefab, _spawnPoint, Quaternion.identity);
            _correctShape.Init(specialShape);
            _correctShape.OnEndInteraction += UseSpecialShape;
            _wrongShape = Instantiate(_specialShapePrefab, _spawnPoint, Quaternion.identity);
            _wrongShape.Init(specialShape + 1);
            _wrongShape.OnEndInteraction += UseSpecialShape;
        }

        /// <summary>
        /// Удаление фигуры по типу.
        /// </summary>
        /// <param name="shapeColor"></param>
        /// <param name="_isAllDeleted"></param>
        public void DeleteShape(ShapeColors shapeColor, bool _isAllDeleted = true)
        {
            _isNeedDeleteShape = false;

            for(int i = 0; i < _shapes.Count; i++)
            {
                if(_shapes[i].ColorType == shapeColor)
                {
                    if (_isAllDeleted)
                    {
                        DeleteShape(i);
                        i--;
                    }
                    else
                    {
                        if (_isNeedDeleteShape)
                        {
                            DeleteShape(i);
                            i--;
                        }
                        _isNeedDeleteShape = !_isNeedDeleteShape;
                    }
                }
            }
        }

        /// <summary>
        /// Удаление фигуры по типу.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="_isAllDeleted"></param>
        public void DeleteShape(ShapeTypes shape, bool _isAllDeleted = true)
        {
            _isNeedDeleteShape = false;

            for (int i = 0; i < _shapes.Count; i++)
            {
                if (_shapes[i].Type == shape)
                {
                    if (_isAllDeleted)
                    {
                        DeleteShape(i);
                        i--;
                    }
                    else
                    {
                        if (_isNeedDeleteShape)
                        {
                            DeleteShape(i);
                            i--;
                        }
                        _isNeedDeleteShape = !_isNeedDeleteShape;
                    }
                }
            }
        }

        /// <summary>
        /// Удаление фигуры по типу.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="shapeColor"></param>
        /// <param name="_isAllDeleted"></param>
        public void DeleteShape(ShapeTypes shape, ShapeColors shapeColor, bool _isAllDeleted = true)
        {
            _isNeedDeleteShape = false;

            for (int i = 0; i < _shapes.Count; i++)
            {
                if (_shapes[i].ColorType == shapeColor && _shapes[i].Type == shape)
                {
                    if (_isAllDeleted)
                    {
                        DeleteShape(i);
                        i--;
                    }
                    else
                    {
                        if (_isNeedDeleteShape)
                        {
                            DeleteShape(i);
                            i--;
                        }
                        _isNeedDeleteShape = !_isNeedDeleteShape;
                    }
                }
            }
        }

        private void Awake()
        {
            _spawnPoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
            _spawnPoint.x -= 1;
            _spawnPoint.z = 0;

            _shapes = new();
            for (int i = 0; i < _StartCountShape; i++)
                CreatedShape();
        }

        private void Update()
        {
            if (_timerCreatedShape > Time.time)
                return;

            if (_shapes.Count < _countShape)
                CreatedShape();
        }

        private void CreatedShape()
        {
            _timerCreatedShape = Time.time + _cooldownCreated;

            ShapeController shape = Instantiate(_shapePrefab, _spawnPoint, Quaternion.identity);
            shape.Init((ShapeTypes)Random.Range(0, Enum.GetValues(typeof(ShapeTypes)).Length),
                (ShapeColors)Random.Range(0, Enum.GetValues(typeof(ShapeColors)).Length));
            shape.OnEnd += DestroyShape;
            _shapes.Add(shape);
        }

        private void DestroyShape(ShapeController shape)
        {
            _timerCreatedShape = Time.time + _cooldownCreated;
            
            for(int i = 0; i < _shapes.Count; i++)
            {
                if(_shapes[i] == shape)
                {
                    _indexShapeForDelete = i;
                    break;
                }
            }
            OnDestroyObject.Invoke(shape.Type, shape.ColorType, shape.EndType);
            DeleteShape(_indexShapeForDelete);
        }

        private void UseSpecialShape(InteractionEndTypes interactionEndType, SpecialShapeTypes specialShapeType)
        {
            OnDestroySpecialObject.Invoke(specialShapeType, interactionEndType);

            DestroySpecialShape();
        }

        private void DestroySpecialShape()
        {
            _correctShape.OnEndInteraction -= UseSpecialShape;
            _wrongShape.OnEndInteraction -= UseSpecialShape;

            Destroy(_correctShape.gameObject);
            Destroy(_wrongShape.gameObject);

            _correctShape = null;
            _wrongShape = null;
        }

        private void DeleteShape(int index)
        {
            _shapes[index].OnEnd -= DestroyShape;
            Destroy(_shapes[index].gameObject);
            _shapes.RemoveAt(index);
        }
    }
}
