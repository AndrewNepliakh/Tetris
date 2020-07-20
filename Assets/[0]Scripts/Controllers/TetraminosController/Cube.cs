using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Cube : MonoBehaviour, IPoolable
{
   private Renderer _renderer;

   public void Initialize(Vector3 position, Color color)
   {
      _renderer = GetComponent<Renderer>();
      _renderer.material.color = color;
      transform.localPosition = position;
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
