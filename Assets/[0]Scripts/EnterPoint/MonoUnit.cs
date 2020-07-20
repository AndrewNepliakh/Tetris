using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonoUnit : MonoBehaviour, IPoolable
{
    public static readonly List<MonoUnit> AllUpdates = new List<MonoUnit>();

    public void UnitStart()
    {
        AllUpdates.Add(this);
        OnStart();
    }
    
    public void OnActivate(object argument = default)
    {
        gameObject.SetActive(true);
        AllUpdates.Add(this);
    }

    public void UnitUpdate()
    {
        OnUpdate();
    }

    public void UnitFixedUpdate()
    {
        OnFixedUpdate();
    }

    protected virtual void OnStart(){}
    protected virtual void OnUpdate(){}
    protected virtual void OnFixedUpdate(){}

    public void OnDeactivate(object argument = default)
    {
        AllUpdates.Remove(this);
        gameObject.SetActive(false);
    }

    
}