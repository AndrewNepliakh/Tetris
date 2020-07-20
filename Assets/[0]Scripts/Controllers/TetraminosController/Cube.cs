using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Cube : MonoUnit
{
   private Renderer _renderer;

   public void Initialize(Vector3 position, Color color)
   {
      _renderer = GetComponent<Renderer>();
      _renderer.material.color = color;
      transform.localPosition = position;
   }
}
