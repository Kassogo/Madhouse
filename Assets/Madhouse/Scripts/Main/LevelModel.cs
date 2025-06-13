using UnityEngine;

[System.Serializable]
public class LevelModel
{
    public string NameIllness => _nameIllness;
    public int IndexScene => _indexScene;

    [SerializeField] private string _nameIllness;
    [SerializeField] private int _indexScene;
}
