using UnityEngine;

public abstract class BaseDialogyModel : ScriptableObject
{
    [SerializeField, TextArea] protected string[] _text;

    public abstract void Play(IDialogueUI ui);
}
