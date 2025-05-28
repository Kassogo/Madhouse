using UnityEngine;

public class MindBreath : MonoBehaviour
{
    private float _timer;
    [SerializeField] GameObject mainCamera;

    private void OnEnable()
    {
        mainCamera.GetComponent<SC_CursorTrail>().enabled = true;
        _timer = 10;
    }
    private void OnDisable()
    {
        Bonuses.instance.mindBreathOn = false;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
            mainCamera.GetComponent<SC_CursorTrail>().enabled = false;
        }
    }
}