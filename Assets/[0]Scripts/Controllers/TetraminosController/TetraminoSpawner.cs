using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TetraminoSpawner
{
    private TetraminoBuilder _tetraminoBuilder = new TetraminoBuilder();
    private TetraminoID? _currentId;
    private TetraminoID _nextId;

    public void Spawn()
    {
       _currentId = _currentId.HasValue ? _nextId :  GetRandomTetraminoID();
       _nextId = GetRandomTetraminoID();
        
        _tetraminoBuilder.BuildTetramino((TetraminoID)_currentId).transform.position = TetraminoController.SpawnPosition;
        EventManager.TriggerEvent(new OnTetraminoSpawnEvent{TetraminoId = _nextId});
    }


    private TetraminoID GetRandomTetraminoID()
    {
        var count = Enum.GetNames(typeof(TetraminoID)).Length;
        return (TetraminoID) Random.Range(0, count);
    }
}