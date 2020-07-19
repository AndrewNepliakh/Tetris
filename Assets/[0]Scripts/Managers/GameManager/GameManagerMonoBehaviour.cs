using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerMonoBehaviour : MonoBehaviour
{
  
    private GameManager _gameManager;
    
    public void SetUp(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Update()
    {
       
    }

    public void DoCoroutine(IEnumerator coroutine)
    {
       
    }
}
