using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI textill;

    public void Set(LevelModel levelModel)
    {
        image.sprite = levelModel.Picture;
        textMesh.text = levelModel.Story;
        textill.text = levelModel.NameIllness;
    }
}
