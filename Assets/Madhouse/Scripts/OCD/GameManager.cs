using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu;


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);

    }

    public void AntiPause()
    {
        PauseMenu.SetActive(false);

    }


}
