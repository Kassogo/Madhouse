using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeController : MonoBehaviour, IPointerClickHandler
{
    private ShapeGenerator _gridGenerator;
    private int _shapeIndex;

    public void Initialize(ShapeGenerator generator, int index)
    {
        _gridGenerator = generator;
        _shapeIndex = index;
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        _gridGenerator.OnShapeClicked(_shapeIndex);
    }

    
}
