using UnityEngine;
using System;

public class OfficeLevel : MonoBehaviour
{
    public event Action OnEndLevel = delegate { };

    [SerializeField] private GameObject _dialogue;
    [SerializeField] private BaseDialogyModel _firstDialogy;
    [SerializeField] private BaseDialogyModel _intercomDialogy;
    [SerializeField] private BaseDialogyModel _endDialogy;

    private IDialogueUI _dialogueUI;

    private void Awake()
    {
        _dialogueUI = _dialogue.GetComponent<IDialogueUI>();
    }

    private void Start()
    {
        _firstDialogy.Play(_dialogueUI);
        _dialogueUI.OnEndDialogue += IntercomTalk;
    }

    private void IntercomTalk()
    {
        _intercomDialogy.Play(_dialogueUI);
        _dialogueUI.OnEndDialogue += EndTalk;
        _dialogueUI.OnEndDialogue -= IntercomTalk;
    }

    private void EndTalk()
    {
        _endDialogy.Play(_dialogueUI);
        _dialogueUI.OnEndDialogue -= EndTalk;
        _dialogueUI.OnEndDialogue += End;
    }

    private void End()
    {
        _dialogueUI.OnEndDialogue -= End;
        OnEndLevel.Invoke();
    }
}
