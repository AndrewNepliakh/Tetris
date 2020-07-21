using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Tetramino : MonoBehaviour, IPoolable
{

    private TetraminoMovementHandler _movementHandler;

    public void Initialize(Vector3 rotationPoint)
    {
        _movementHandler = new TetraminoMovementHandler(this, rotationPoint);
    }
    
    public void OnActivate(object argument = default)
    {
        gameObject.SetActive(true);
        enabled = true;
    }

    private void Update()
    {
        _movementHandler.Move();
        _movementHandler.Fall();
        _movementHandler.Rotate();
    }
    
    public void OnDeactivate(object argument = default)
    {
        var controller = (TetraminoController) argument;
        enabled = false;
        
        for (int i = transform.childCount -1; i >= 0 ; i--)
        {
            var child = transform.GetChild(i).GetComponent<Cube>();
            controller.SetGrid(child);
            child.transform.SetParent(controller.CubeParent);
        }
        
        gameObject.SetActive(false);
    }
    
    
}
