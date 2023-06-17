using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public bool CanMove { get; private set; } = true; 
    
    public void SetCanMove(bool variable)
    {
        CanMove = variable;
    }
    

    private void Awake()
    {
        Instance = this;
    }

}
