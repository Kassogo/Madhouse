using UnityEngine;

[System.Serializable]
public class LevelModel
{
    public string NameIllness => _nameIllness;
    public int IndexScene => _indexScene;

    public Sprite Picture => _sprite;

    public string Story => _story;

    [SerializeField] private string _nameIllness;
    [SerializeField] private int _indexScene;
    [SerializeField] private Sprite _sprite;
    [TextArea(0,50)]
    [SerializeField] private string _story;
}
