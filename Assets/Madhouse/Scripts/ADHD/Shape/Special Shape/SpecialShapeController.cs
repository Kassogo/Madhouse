using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    public class SpecialShapeController : MonoBehaviour
    {
        public event Action<InteractionEndTypes, SpecialShapeTypes> OnEndInteraction = delegate { };

        public SpecialShapeTypes Type => _type;

        [SerializeField] private ShapeInteraction _shapeInteraction;
        [SerializeField] private SpecialShapeData _specialShapeData;
        [SerializeField] private SpriteRenderer _spriteShape;

        private SpecialShapeTypes _type;

        public void Init(SpecialShapeTypes shape)
        {
            _type = shape;
            _shapeInteraction.OnInteractEnd += EndInteraction;

            for (int i = 0; i < _specialShapeData.SpacialShapes.Count; i++)
            {
                if (_specialShapeData.SpacialShapes[i].Type == _type)
                {
                    _spriteShape.sprite = _specialShapeData.SpacialShapes[i].Picture;
                    break;
                }
            }
        }

        private void EndInteraction(InteractionEndTypes interactionEndType)
        {
            OnEndInteraction.Invoke(interactionEndType, _type);
        }

        private void OnDestroy()
        {
            _shapeInteraction.OnInteractEnd -= EndInteraction;
        }
    }
}
