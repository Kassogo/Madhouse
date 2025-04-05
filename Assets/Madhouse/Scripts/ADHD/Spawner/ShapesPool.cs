using System;
using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Пул обджект для фигур.
    /// </summary>
    public class ShapesPool
    {
        public event Action<ShapeTypes, ShapeColors, InteractionEndTypes> OnUseShape = delegate { };

        private Stack<ShapeController> _deactivateShapes;
        private ShapeController _prefab;
        private int _initialSize;
        private bool _isExpandable;
        private List<ShapeController> _poolActivateList = new List<ShapeController>();

        /// <summary>
        /// Кол-во активных фигур.
        /// </summary>
        public int CountActivateShape => _poolActivateList.Count;

        public ShapesPool(ShapeController shape, int maxSize, bool isExpanded = true)
        {
            _prefab = shape;
            _isExpandable = isExpanded;
            _initialSize = maxSize;
            _deactivateShapes = new();
            _poolActivateList = new();

            for (int i = 0; i < _initialSize; i++)
            {
                ShapeController createdShape = CreateNewPooledObject();
                createdShape.gameObject.SetActive(false);
                _deactivateShapes.Push(createdShape);
            }
        }

        /// <summary>
        /// Получение фигуры.
        /// </summary>
        /// <returns>Отключённая фигура.</returns>
        public ShapeController GetFromPool()
        {
            ShapeController returnShape;
            if (_deactivateShapes.Count != 0)
            {
                returnShape = _deactivateShapes.Pop();
                _poolActivateList.Add(returnShape);
                returnShape.OnEnd += OverUsedShape;
                return returnShape;
            }

            if (_isExpandable)
            {
                returnShape = CreateNewPooledObject();
                _poolActivateList.Add(returnShape);
                returnShape.OnEnd += OverUsedShape;
                return returnShape;
            }

            Debug.LogWarning("Пул заполнен, и расширение отключено.");
            return null;
        }

        /// <summary>
        /// Возвращение фигуры.
        /// </summary>
        /// <param name="shape"></param>
        public void ReturnToPool(ShapeController shape)
        {
            if (_poolActivateList.Contains(shape))
            {
                for (int i = 0; i < _poolActivateList.Count; i++)
                {
                    if (_poolActivateList[i] == shape)
                    {
                        _poolActivateList.RemoveAt(i);
                        shape.gameObject.SetActive(false);
                        shape.OnEnd -= OverUsedShape;
                        _deactivateShapes.Push(shape);
                        break;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Этот объект не принадлежит пулу.");
            }
        }

        /// <summary>
        /// Разрушение фигур.
        /// </summary>
        public void Disable()
        {
            foreach (ShapeController shape in _poolActivateList)
                shape.OnEnd -= OverUsedShape;
        }

        /// <summary>
        /// Деактивация фигур с условием.
        /// </summary>
        /// <param name="condition"></param>
        public void DeactivateByCondition(Predicate<ShapeController> condition)
        {
            for (int i = 0; i < _poolActivateList.Count; i++)
            {
                if (condition(_poolActivateList[i]))
                {
                    ReturnToPool(_poolActivateList[i]);
                    i--;
                }
            }
        }

        private ShapeController CreateNewPooledObject()
        {
            return GameObject.Instantiate(_prefab);
        }

        private void OverUsedShape(ShapeController shape)
        {
            OnUseShape.Invoke(shape.Type, shape.ColorType, shape.EndType);
            ReturnToPool(shape);
        }
    }
}
