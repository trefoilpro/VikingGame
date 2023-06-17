using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    public int ScoreNumber { get; private set; }

    public void AddScore(int score)
    {
        ScoreNumber += score;
        _scoreText.text = ScoreNumber.ToString();
    }

    public void ResetScore()
    {
        ScoreNumber = 0;
        _scoreText.text = ScoreNumber.ToString();
    }
}
