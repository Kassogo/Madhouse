using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Пул обджект для специальных фигур.
    /// </summary>
    public class SpecialShapePool
    {
        /// <summary>
        /// Событие использования специальной фигуры.
        /// </summary>
        public Action<InteractionEndTypes, SpecialShapeTypes> OnUseSpecialObject = delegate { };

        private SpecialShapeController _prefab;
        private SpecialShapeController _correctShape;
        private SpecialShapeController _wrongShape;

        public SpecialShapePool(SpecialShapeController prefab)
        {
            _prefab = prefab;
            _correctShape = GameObject.Instantiate(_prefab);
            _correctShape.OnEndInteraction += OverSpecialShape;
            _wrongShape = GameObject.Instantiate(_prefab);
            _wrongShape.OnEndInteraction += OverSpecialShape;
            DeactivateSpecialShape();
        }

        /// <summary>
        /// Активация специальных фигур.
        /// </summary>
        /// <param name="spawnPosition"></param>
        /// <param name="specialShapeTypes"></param>
        public void ActivateSpecialShape(Vector3 spawnPosition, SpecialShapeTypes specialShapeTypes)
        {
            if (_correctShape.isActiveAndEnabled || _wrongShape.isActiveAndEnabled)
                DeactivateSpecialShape();

            _correctShape.transform.position = spawnPosition;
            _wrongShape.transform.position = spawnPosition;
            _wrongShape.gameObject.SetActive(true);
            _correctShape.gameObject.SetActive(true);
            _correctShape.Init(specialShapeTypes);
            _wrongShape.Init(specialShapeTypes + 1);
        }

        /// <summary>
        /// Уничтожение специальных фигур.
        /// </summary>
        public void Disable()
        {
            _correctShape.OnEndInteraction -= OverSpecialShape;
            _wrongShape.OnEndInteraction -= OverSpecialShape;
        }

        private void OverSpecialShape(InteractionEndTypes interactionEndTypes, SpecialShapeTypes specialShapeTypes)
        {
            OnUseSpecialObject.Invoke(interactionEndTypes, specialShapeTypes);
            DeactivateSpecialShape();
        }

        private void DeactivateSpecialShape()
        {
            _correctShape.gameObject.SetActive(false);
            _wrongShape.gameObject.SetActive(false);
        }
    }
}
