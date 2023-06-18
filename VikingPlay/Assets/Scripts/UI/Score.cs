using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    public int ScoreNumber { get; private set; }

    public void AddScore(int score)
    {
        ScoreNumber += score;
        
        if (ScoreNumber > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", ScoreNumber);
        }

        _scoreText.text = ScoreNumber.ToString();
    }
}
