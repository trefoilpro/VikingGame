using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    [SerializeField] private MainMenuBackGround _mainMenuBackGround;


    
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _mainMenuBackGround.CreateBackGround();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void UIGame()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
