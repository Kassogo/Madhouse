using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineLevel : MonoBehaviour
{
    public event Action<LevelModel> OnTouchLineLevel = delegate { };

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Button _buttonActivation;

    private LevelModel _levelModel;

    public void Init(LevelModel levelModel)
    {
        _levelModel = levelModel;
        _nameText.text = _levelModel.NameIllness;
    }

    private void Awake()
    {
        _buttonActivation.onClick.AddListener(Touch);
    }

    private void OnDestroy()
    {
        _buttonActivation.onClick.RemoveListener(Touch);
    }

    private void Touch() => OnTouchLineLevel.Invoke(_levelModel);
}
