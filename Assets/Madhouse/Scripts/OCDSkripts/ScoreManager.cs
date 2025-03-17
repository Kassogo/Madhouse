using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; set; }
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;

    private void Start()
    {
        Instance = this;
    }


    public void SetScore(int score)
    {
        ScoreManager.Instance._score += score;
        _scoreText.text = "Score: " + this._score;
    }



   /*
   public Text scoreText;

   public static int score;

   private void Update()
   {
       scoreText.text = "Score" + score;
   }
   */ 
}
