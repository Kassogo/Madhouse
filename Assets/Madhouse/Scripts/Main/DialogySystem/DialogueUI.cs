using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour, IDialogueUI
{
    public event Action OnEndDialogue = delegate { };

    [SerializeField] private GameObject _panelDialogue;
    [SerializeField] private GameObject _panelName;
    [Space]
    [SerializeField] private TextMeshProUGUI _dialogue;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _portrait;

    private int _indexText;
    private string[] _text;

    private bool _isDialogy = false;
    private bool _isMeSpeakeNow = true;
    private string _nameInterlocutor;
    private string _ourname;

    public void ShowCharacterDialogue(string ourName, string name, Sprite portrait, string[] text)
    {
        _isDialogy = true;
        _isMeSpeakeNow = true;
        _indexText = 0;
        _text = text;
        _nameInterlocutor = name;
        _ourname = ourName;
        _dialogue.text = _text[_indexText];
        _name.text = _ourname;
        _portrait.sprite = portrait;
        _panelDialogue.SetActive(true);
        _panelName.SetActive(true);
        _portrait.gameObject.SetActive(true);
    }

    public void ShowNamedDialogue(string name, string[] text)
    {
        _indexText = 0;
        _text = text;
        _dialogue.text = _text[_indexText];
        _name.text = name;
        _panelDialogue.SetActive(true);
        _panelName.SetActive(true);
        _portrait.gameObject.SetActive(false);
    }

    public void ShowSelfDialogue(string[] text)
    {
        _indexText = 0;
        _text = text;
        _dialogue.text = _text[_indexText];
        _panelDialogue.SetActive(true);
        _panelName.SetActive(false);
        _portrait.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _panelDialogue.SetActive(false);
        _panelName.SetActive(false);
        _portrait.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _text != null)
            ShowNextText();
    }

    private void ShowNextText()
    {
        _indexText++;
        if (_indexText == _text.Length)
        {
            EndingDialogue();
            return;
        }

        if (_isDialogy)
        {
            _isMeSpeakeNow = !_isMeSpeakeNow;
            _name.text = _isMeSpeakeNow ? _ourname : _nameInterlocutor;
        }
        _dialogue.text = _text[_indexText];
    }

    private void EndingDialogue()
    {
        _isDialogy = false;
        _text = null;
        _panelDialogue.SetActive(false);
        _panelName.SetActive(false);
        _portrait.gameObject.SetActive(false);
        OnEndDialogue.Invoke();
    }
}
