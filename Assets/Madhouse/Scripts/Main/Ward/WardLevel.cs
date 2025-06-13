using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardLevel : MonoBehaviour
{
    [SerializeField] private Patient[] _patients;
    [SerializeField] private DialogueUI _dialogueUI;

    private void Start()
    {
        for (int i = 0; i < _patients.Length; i++)
        {
            _patients[i].OnStartDialogy += ShowDialogy;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _patients.Length; i++)
        {
            _patients[i].OnStartDialogy -= ShowDialogy;
        }
    }

    private void ShowDialogy(BaseDialogyModel dialogyModel)
    {
        dialogyModel.Play(_dialogueUI as IDialogueUI);
    }
}
