using UnityEngine;

public class ShapeController : MonoBehaviour
{
    private PuzzleController _puzzleController;
    private int _shapeIndex;
    private Collider2D _collider; // ��� �����������

    void Awake()
    {
        _collider = GetComponent<Collider2D>(); // �������� ��������� ���� ���
        if (_collider == null)
        {
            Debug.LogWarning("ShapeController �� ����� Collider2D, ����� �� ����� ��������!", this);
        }
    }

    /// <summary>
    /// ������������� ������ ������������.
    /// </summary>
    /// <param name="controller">������ �� ������� ����������.</param>
    /// <param name="index">������ ���� ������ � �����.</param>
    public void Initialize(PuzzleController controller, int index)
    {
        _puzzleController = controller;
        _shapeIndex = index;
    }

    // ���������� Unity ��� ����� ������ (������� Collider �� �������)
    private void OnMouseDown()
    {
        if (_puzzleController != null)
        {
            // �������� �����������, ��� �� ���� ������ ��������
            _puzzleController.OnShapeClicked(_shapeIndex);
        }
        else
        {
            Debug.LogWarning("PuzzleController �� ��������������� ��� ���� ������!", this);
        }
    }
}
