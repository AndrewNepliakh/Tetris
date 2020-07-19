using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneEnterPoint : Singleton<SceneEnterPoint>
{
    [SerializeField] private List<BaseInjectable> Injectables = new List<BaseInjectable>();
    private static List<MonoUnit> _monoUnits = new List<MonoUnit>();
    
    private void Awake()
    {
        foreach (var inject in Injectables)
        {
            InjectBox.Add(inject);
        }
        
        InjectBox.InitializeStartInjectables();
        
        _monoUnits = FindObjectsOfType<MonoUnit>().ToList();
    }

    private void Start()
    {
        foreach (var entity in _monoUnits)
        {
            entity.UnitStart();
        }
    }
    
    private void OnApplicationQuit()
    {
        InjectBox.DisableAll();
    }
}