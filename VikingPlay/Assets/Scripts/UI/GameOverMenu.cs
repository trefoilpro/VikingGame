using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    
    public void ShowGameOverMenu()
    {
        _score.gameObject.SetActive(false);
        SetScoreText();
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    private void SetScoreText()
    {
        _scoreText.text = "Your Score: " + _score.ScoreNumber;
        
        
        
        _highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }
    
    
    
}
