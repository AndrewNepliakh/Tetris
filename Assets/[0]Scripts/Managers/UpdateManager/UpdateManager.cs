using System;
using UnityEngine;


[CreateAssetMenu(fileName = "UpdateManager", menuName = "Managers/UpdateManager")]
public class UpdateManager : BaseInjectable, IAwake
{
    public void OnAwake()
    {
        GameObject.Find("[EnterPoint]").GetComponent<UpdateManagerMonoBehaviour>().SetUp(this);
    }

    public void Update()
    {
        foreach (var unit in MonoUnit.AllUpdates)
        {
            unit.UnitUpdate();
        }
    }

    public void FixedUpdate()
    {
        foreach (var entity in MonoUnit.AllUpdates)
        {
            entity.UnitFixedUpdate();
        }
    }
}