using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetramino : MonoBehaviour
{
    private const int HEIGHT = 20;
    private const int WIDTH = 10;

    [SerializeField] 
    private Vector3 _rotationPoint;

    private float _previousTime;
    private float _fallTime = 0.8f;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            if(!IsInBorders()) transform.position -= Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            if(!IsInBorders()) transform.position -= Vector3.right;
        }

        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? _fallTime / 10 : _fallTime))
        {
            transform.position += Vector3.down;
            if(!IsInBorders()) transform.position -= Vector3.down;
            _previousTime = Time.time;
        }
        
        Debug.Log(IsInBorders());
    }

    private bool IsInBorders()
    {
        foreach (Transform child in transform)
        {
            var position = child.transform.position;
            var roundedX = (int)position.x;
            var roundedY = (int)position.y;

            if (roundedX < 0 || roundedX >= WIDTH || roundedY < 0 || roundedY >= HEIGHT) return false;
        }

        return true;
    }

}
