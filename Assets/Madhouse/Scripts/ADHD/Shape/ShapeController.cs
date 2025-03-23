using System;
using System.Collections;
using UnityEngine;

namespace Madhouse.ADHD
{
    public class ShapeController : MonoBehaviour
    {
        public event Action<ShapeController> OnEnd = delegate { };

        public ShapeTypes Type => _shapeType;
        public ShapeColors ColorType => _color;
        public InteractionEndTypes EndType => _endtype;

        [SerializeField] private SpriteRenderer _spriteShape;
        [SerializeField] private SpriteRenderer _spriteGlare;
        [SerializeField] private ShapesData _shapesData;
        [SerializeField] private ColorsData _colorsData;
        [SerializeField] private ShapeInteraction _shapeInteraction;
        [SerializeField] private float _minTimeOverTask = 8;
        [SerializeField] private float _maxTimeOverTask = 15;

        private ShapeTypes _shapeType;
        private ShapeColors _color;
        private InteractionEndTypes _endtype;


        public void Init(ShapeTypes shape, ShapeColors color)
        {
            _shapeType = shape;
            _shapeInteraction.OnInteractEnd += HaveEndInteraction;
            _color = color;

            for(int i = 0; i <_shapesData.Shapes.Count; i++)
            {
                if(_shapesData.Shapes[i].Type == _shapeType)
                {
                    _spriteGlare.sprite = _shapesData.Shapes[i].Glare;
                    _spriteShape.sprite = _shapesData.Shapes[i].Picture;
                    break;
                }
            }

            for (int i = 0; i < _colorsData.Colors.Count; i++)
            {
                if (_colorsData.Colors[i].ColorType == color)
                {
                    _spriteShape.color = _colorsData.Colors[i].ColorValue;
                    break;
                }
            }
        }

        private void HaveEndInteraction(InteractionEndTypes interaction)
        {
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
