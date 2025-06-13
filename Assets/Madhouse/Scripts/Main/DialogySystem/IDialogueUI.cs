using System;
using UnityEngine;

public interface IDialogueUI
{
    public event Action OnEndDialogue;
    public void ShowSelfDialogue(string[] text);
    public void ShowNamedDialogue(string name, string[] text);
    public void ShowCharacterDialogue(string ourName, string name, Sprite portrait, string[] text);
}
