using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Система перемещения фигур.
    /// </summary>
    public class ShapeDragger : MonoBehaviour
    {
        private Camera _mainCamera;
        private ShapeInteraction _selectedShape;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                TryPickShape();

            if (_selectedShape == null)
                return;

            if (Input.GetMouseButton(0))
                DragShape();

            if (Input.GetMouseButtonDown(1))
                CancelDrag();

            if (Input.GetMouseButtonUp(0))
                DropShape();
        }

        private void TryPickShape()
        {
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                _selectedShape = hit.collider.GetComponent<ShapeInteraction>();
                _selectedShape.Pick();
            }
        }

        private void DragShape()
        {
            _selectedShape.Drag();
        }

        private void DropShape()
        {
            _selectedShape.StopDrag();
            _selectedShape = null;
        }

        private void CancelDrag()
        {
            _selectedShape.CancelDrag();
            _selectedShape = null;
        }
    }
}
