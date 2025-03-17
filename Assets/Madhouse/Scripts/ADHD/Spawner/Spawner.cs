using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    public class Spawner : MonoBehaviour
    {
        public event System.Action<ShapeTypes, ShapeColors, InteractionEndTypes> OnDestroyObject = delegate { }; 
        [SerializeField] private ShapeController _shapePrefab;
        [SerializeField] private int _StartCountShape;
        [SerializeField] private int _countShape;

        private List<ShapeController> _shapes;
        private float _cooldownCreated = 2f;
        private float _timerCreatedShape;

        private Vector3 _spawnPoint;

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
            var shape = Instantiate(_shapePrefab, _spawnPoint, Quaternion.identity);
            shape.Init((ShapeTypes)Random.Range(0, System.Enum.GetValues(typeof(ShapeTypes)).Length),
                (ShapeColors)Random.Range(0, System.Enum.GetValues(typeof(ShapeColors)).Length));
            shape.OnEnd += DestroyShape;
            _shapes.Add(shape);
        }

        private void DestroyShape(ShapeController shape)
        {
            _timerCreatedShape = Time.time + _cooldownCreated;
            shape.OnEnd -= DestroyShape;
            for(int i = 0; i < _shapes.Count; i++)
            {
                if(_shapes[i] == shape)
                {
                    _shapes.RemoveAt(i);
                    break;
                }
            }
            OnDestroyObject.Invoke(shape.Type, shape.ColorType, shape.EndType);
            Destroy(shape.gameObject);
        }
    }
}
