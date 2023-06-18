using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
