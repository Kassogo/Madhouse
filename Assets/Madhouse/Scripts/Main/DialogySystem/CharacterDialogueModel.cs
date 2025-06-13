using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/" + nameof(CharacterDialogueModel))]
public class CharacterDialogueModel : BaseDialogyModel
{
    [SerializeField] private string _nameInterlocutor;
    [SerializeField] private string _ourName;
    [SerializeField] private Sprite _portrait;

    public override void Play(IDialogueUI ui)
    {
        ui.ShowCharacterDialogue(_ourName, _nameInterlocutor, _portrait, _text);
    }
}
