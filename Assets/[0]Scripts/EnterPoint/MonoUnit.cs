using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonoUnit : MonoBehaviour, IDisable
{
    public static readonly List<MonoUnit> AllUpdates = new List<MonoUnit>();

    private void OnEnable()
    {
        AllUpdates.Add(this);
        OnTurnOn();
    }

    private void OnDisable()
    {
        AllUpdates.Remove(this);
        OnTurnOff();
    }
    
    public void UnitStart()
    {
        OnStart();
    }

    public void UnitUpdate()
    {
        OnUpdate();
    }

    public void UnitFixedUpdate()
    {
        OnFixedUpdate();
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnFixedUpdate()
    {
        
    }

    protected virtual void OnTurnOn()
    {
    }

    protected virtual void OnTurnOff()
    {
    }

    public void LocalDisable()
    {
        OnTurnOff();
    }
}