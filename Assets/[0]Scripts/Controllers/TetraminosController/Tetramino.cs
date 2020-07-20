using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Tetramino : MonoUnit
{
    private Vector3 _rotationPoint;
    private TetraminoMovementHandler _movementHandler;
    
    public void Initialize(Vector3 rotationPoint)
    {
        _movementHandler = new TetraminoMovementHandler(this, _rotationPoint);
        _rotationPoint = rotationPoint;
    }

    protected override void OnUpdate()
    {
        _movementHandler.Move();
        _movementHandler.Fall();
        _movementHandler.Rotate();
    }
}
