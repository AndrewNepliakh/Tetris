using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TetraminoID
{
    I_tetramino = 0,
    L_tetramino,
    J_tetramino,
    O_tetramino,
    S_tetramino,
    Z_tetramino,
    T_tetramino
}

[Serializable]
public class TetraminoModel
{
    public TetraminoID Id;
    public Sprite icon;
    public Vector3[] positionMatrix;
    public Vector3 rotationPoint;
    public Color color;
}


[CreateAssetMenu(fileName = "TetraminoData", menuName = "Data/TetraminoData")]
public class TetraminoData : BaseInjectable
{
    public List<TetraminoModel> TetraminoModels = new List<TetraminoModel>();

    public Color GetColor(TetraminoID id)
    {
        return TetraminoModels.Find(model => model.Id == id).color;
    }

    public Vector3[] GetMatrix(TetraminoID id)
    {
        return TetraminoModels.Find(model => model.Id == id).positionMatrix;
    }
    
    public Vector3 GetRotationPoint(TetraminoID id)
    {
        return TetraminoModels.Find(model => model.Id == id).rotationPoint;
    }
    
    public Sprite GetIcon(TetraminoID id)
    {
        return TetraminoModels.Find(model => model.Id == id).icon;
    }
}

