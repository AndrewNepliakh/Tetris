using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraminoSpawner
{
    private TetraminoBuilder _tetraminoBuilder = new TetraminoBuilder();

    public void Spawn()
    {
        _tetraminoBuilder.BuildTetramino().transform.position = TetraminoController.SpawnPosition;
    }
}