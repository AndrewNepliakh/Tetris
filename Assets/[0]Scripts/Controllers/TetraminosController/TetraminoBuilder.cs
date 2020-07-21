using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TetraminoBuilder
{
    private const int CUBES_PER_TETRAMINO = 4;

    private GameObject _cubePrefab;
    private Transform _parentChest;

    private PoolManager _poolManager;
    private TetraminoData _tetraminoData;

    public TetraminoBuilder()
    {
        _cubePrefab = Resources.Load<GameObject>("Prefabs/SceneItems/Cube/Cube");
        _parentChest = GameObject.Find("Chest").GetComponent<Transform>();

        _poolManager = InjectBox.Get<PoolManager>();
        _tetraminoData = InjectBox.Get<TetraminoData>();
    }

    public Tetramino BuildTetramino(TetraminoID id)
    {
        var tetraminoId = id;
        var rotationPoint = _tetraminoData.GetRotationPoint(tetraminoId);
        
        var tetramino = _poolManager.GetOrCreate<Tetramino>(_parentChest);
        tetramino.transform.SetParent(_parentChest);
        tetramino.Initialize(rotationPoint);

        for (int i = 0; i < CUBES_PER_TETRAMINO; i++)
        {
            var cube = _poolManager.GetOrCreate<Cube>(_cubePrefab, tetramino.transform);
            cube.transform.SetParent(tetramino.transform);
            var color = _tetraminoData.GetColor(tetraminoId);
            var position = _tetraminoData.GetMatrix(tetraminoId)[i];
            cube.Initialize(position,color);
        }

        return tetramino;
    }
}