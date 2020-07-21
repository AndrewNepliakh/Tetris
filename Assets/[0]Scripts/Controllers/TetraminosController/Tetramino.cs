using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Tetramino : MonoBehaviour, IPoolable
{
    private Vector3 _rotationPoint;
    private TetraminoMovementHandler _movementHandler;

    public void Initialize(Vector3 rotationPoint)
    {
        _movementHandler = new TetraminoMovementHandler(this, _rotationPoint);
        _rotationPoint = rotationPoint;
    }

    private void Update()
    {
        _movementHandler.Move();
        _movementHandler.Fall();
        _movementHandler.Rotate();
    }

    public void OnActivate(object argument = default)
    {
       gameObject.SetActive(true);
       enabled = true;
    }

    public void OnDeactivate(object argument = default)
    {
        for (int i = transform.childCount -1; i >= 0 ; i--)
        {
            transform.GetChild(i).SetParent((Transform)argument);
        }
        
        gameObject.SetActive(false);
    }
    
    
}
