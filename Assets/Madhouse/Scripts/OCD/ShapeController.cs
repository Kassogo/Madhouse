using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeController : MonoBehaviour, IPointerClickHandler
{
    private ShapeGenerator gridGenerator;
    private int shapeIndex;

    public void Initialize(ShapeGenerator generator, int index)
    {
        gridGenerator = generator;
        shapeIndex = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gridGenerator.OnShapeClicked(shapeIndex);
    }
}
