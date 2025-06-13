using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/" + nameof(SelfDialogueModel))]
public class SelfDialogueModel : BaseDialogyModel
{
    public override void Play(IDialogueUI ui)
    {
        ui.ShowSelfDialogue(_text);
    }
}
