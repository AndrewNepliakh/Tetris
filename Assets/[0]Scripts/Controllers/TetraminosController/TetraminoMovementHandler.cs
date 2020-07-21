using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraminoMovementHandler
{
    private Tetramino _tetramino;
    
    private Vector3 _rotationPoint;

    private readonly float HEIGHT = TetraminoController.Height;
    private readonly float WIDTH = TetraminoController.Width;
    
    private float _previousTime;
    private float _fallTime = 0.8f;

    public TetraminoMovementHandler(Tetramino tetramino, Vector3 rotationPoint)
    {
        _tetramino = tetramino;
        _rotationPoint = rotationPoint;
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _tetramino.transform.position += Vector3.left;
            if(IsMovementConstrained()) _tetramino.transform.position -= Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _tetramino.transform.position += Vector3.right;
            if(IsMovementConstrained()) _tetramino.transform.position -= Vector3.right;
        }
    }

    public void Fall()
    {
        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? _fallTime / 10 : _fallTime))
        {
            _tetramino.transform.position += Vector3.down;
            if (IsMovementConstrained())
            {
                _tetramino.transform.position -= Vector3.down;
                EventManager.TriggerEvent(new OnTetraminoFellEvent {Tetramino = _tetramino});
            }
            
            _previousTime = Time.time;
        }
    }

    public void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _tetramino.transform.RotateAround(_tetramino.transform.TransformPoint(_rotationPoint), Vector3.forward, 90);
            if(IsMovementConstrained()) 
                _tetramino.transform.RotateAround(_tetramino.transform.TransformPoint(_rotationPoint), Vector3.forward, -90);
        }
    }

    private bool IsMovementConstrained()
    {
        foreach (Transform child in _tetramino.transform)
        {
            var position = child.transform.position;
            var roundedX = Mathf.RoundToInt(position.x);
            var roundedY = Mathf.RoundToInt(position.y);

            if (roundedX < 0 || roundedX >= WIDTH || roundedY < 0) return true;

            if (TetraminoController.Grid[roundedX, roundedY]) return true;
        }
        return false;
    }

}
