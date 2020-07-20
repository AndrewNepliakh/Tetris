using System;
using UnityEngine;
/// <summary>
/// Class that handle all Updates in one place
/// </summary>
[CreateAssetMenu(fileName = "UpdateManager", menuName = "Managers/UpdateManager")]
public class UpdateManager : BaseInjectable, IAwake, IGlobal
{
    public void OnAwake()
    {
        try
        {
            GameObject.Find("[Singleton: INJECTBOX]").GetComponent<UpdateManagerMonobehaviour>().SetUp(this);
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("UpdateManagerMonoBehaviour doesn't exist");
        }
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