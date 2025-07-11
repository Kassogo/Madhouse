using Madhouse.AnxietyDisorder;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public static Panel instance;

    [SerializeField] GameObject _panelTextGO;
    [SerializeField] GameObject _menuButton;
    [SerializeField] GameObject _replayButton;

    public bool TheEnd;

    private Image _panel;
    private TextMeshProUGUI _panelText;
    private string _text1;
    private string _text2;

    private int _currentScene;


    private void Awake()
    {
        TheEnd = false;
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        instance = this;
        _panel = GetComponent<Image>();
        _panelText = _panelTextGO.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _panelTextGO.SetActive(false);
        _menuButton.SetActive(false);
        _replayButton.SetActive(false);
        _text1 = "Ты сильнее, чем кажется.\r\nЕщё раз?";
        _text2 = "У тебя получилось!";
        StartCoroutine(FadeOut());
    }
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float alphaVal = 0;
        Color _color = _panel.color;
        _color.a = alphaVal;
        _panel.color = _color;

        while (_panel.color.a < 1)
        {
            alphaVal += 0.01f;
            _color.a = alphaVal;
            _panel.color = _color;
            yield return new WaitForSeconds(0.01f);
        }
        HealthBar.instance._maxVignette = 0;
        HealthBar.instance._updateHealthBar();
        HealthBar.instance.gameProcess = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartPanelText());
    }
    public void Replay()
    {
        SoundManager.instance.ButtonClick();
        SceneManager.LoadScene(_currentScene);
    }
    public void Menu()
    {
        SoundManager.instance.ButtonClick();
        SceneManager.LoadScene(0);
    }

    public void PauseOn()
    {
        SoundManager.instance.ButtonClick();

        _panelTextGO.SetActive(true);

        Color _color = _panel.color;
        _color.a = 0.5f;
        _panel.color = _color;

        _panelText.text = "Передышка";

        _menuButton.SetActive(true);
        _replayButton.SetActive(true);
    }

    public void PauseOff()
    {
        SoundManager.instance.ButtonClick();

        Color _color = _panel.color;
        _color.a = 0;
        _panel.color = _color;

        _panelTextGO.SetActive(false);
        _menuButton.SetActive(false);
        _replayButton.SetActive(false);
    }

    IEnumerator StartPanelText()
    {
        _panelTextGO.SetActive(true);
        _panelText.text = "";

        TheEnd = true;

        if (HealthBar.instance._lose == true)
        {
            for (int i = 0; i < _text1.Length - 8; i++)
            {
                _panelText.text += _text1[i];
                SoundManager.instance.Letter();
                yield return new WaitForSeconds(0.05f);
                SoundManager.instance.StopLetter();
            }
            yield return new WaitForSeconds(1f);
            for (int i = 24; i < _text1.Length; i++)
            {
                _panelText.text += _text1[i];
                SoundManager.instance.Letter();
                yield return new WaitForSeconds(0.05f);
                SoundManager.instance.StopLetter();
            }
        }
        else
        {
            for (int i = 0; i < _text2.Length; i++)
            {
                _panelText.text += _text2[i];
                SoundManager.instance.Letter();
                yield return new WaitForSeconds(0.05f);
                SoundManager.instance.StopLetter();
            }
            yield return new WaitForSeconds(1f);
        }

        _menuButton.SetActive(true);
        _replayButton.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        float alphaVal = 1;
        Color _color = _panel.color;
        _color.a = alphaVal;
        _panel.color = _color;

        yield return new WaitForSeconds(0.5f);

        while (_panel.color.a > 0)
        {
            alphaVal -= 0.01f;
            _color.a = alphaVal;
            _panel.color = _color;
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
    }
}
