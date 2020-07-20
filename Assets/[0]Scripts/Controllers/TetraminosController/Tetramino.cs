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
    }

    public void OnDeactivate(object argument = default)
    {
        gameObject.SetActive(false);
    }
}
