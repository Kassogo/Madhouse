using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] GameObject _panelGO;

    public bool onPause;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        onPause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onPause == false)
            {
                _panelGO.SetActive(true);
                Panel.instance.PauseOn();
                onPause = true;
                Time.timeScale = 0;
            }
            else
            {
                Panel.instance.PauseOff();
                onPause = false;
                _panelGO.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
