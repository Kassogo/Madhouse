using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public static int score;

    private void Update()
    {
        scoreText.text = "Score" + score;
    }


}
