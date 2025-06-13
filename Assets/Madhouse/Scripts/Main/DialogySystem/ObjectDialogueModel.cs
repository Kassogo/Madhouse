using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/" + nameof(ObjectDialogueModel))]
public class ObjectDialogueModel : BaseDialogyModel
{
    [SerializeField] private string _nameObject;

    public override void Play(IDialogueUI ui)
    {
        ui.ShowNamedDialogue(_nameObject, _text);
    }
}
