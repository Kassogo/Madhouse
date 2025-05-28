using UnityEngine;

public class BrainBody : MonoBehaviour
{
    [SerializeField] GameObject circle;
    [SerializeField] GameObject prints;
    public float rotateSpeed;
    public float circleRotateSpeed;
    public float printsRotateSpeed;

    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed / 100);
        circle.transform.Rotate(0, 0, circleRotateSpeed / 100);
        prints.transform.Rotate(0, 0, printsRotateSpeed / -100);
    }
}
