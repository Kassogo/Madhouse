using System;
using UnityEngine;
using UnityEngine.UI;

public class Patient : MonoBehaviour
{
    public event Action<BaseDialogyModel> OnStartDialogy = delegate { };

    [SerializeField] private BaseDialogyModel _dialogy;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartDialogy);
    }

    private void OnDestroy()
    {
        _button.onClick?.RemoveListener(StartDialogy);
    }

    private void StartDialogy()
    {
        OnStartDialogy.Invoke(_dialogy);
        _button.onClick.RemoveListener(StartDialogy);
    }
}
