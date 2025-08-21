using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        ShowTutorial();
        _closeButton.onClick.AddListener(HideTutorial);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(HideTutorial);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Time.timeScale == 1)
                ShowTutorial();
            else
                HideTutorial();
        }
    }

    private void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void HideTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
