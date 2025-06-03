using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Button _buttonExit;

    private void Awake()
    {
        if (_buttonExit == null)
            _buttonExit = GetComponent<Button>();
        _buttonExit.onClick.AddListener(Exit);
    }

    private void OnDestroy()
    {
        _buttonExit.onClick.RemoveListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
