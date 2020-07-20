using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to handle all processes of level stage
/// </summary>

[CreateAssetMenu(fileName = "LevelManager", menuName = "Managers/LevelManager")]
public class LevelManager : BaseInjectable, IAwake, IStart, IDisable
{
    private TetraminoController _tetraminoController;
    
    public void OnAwake()
    {
       _tetraminoController = new TetraminoController();
    }

    public void OnStart()
    {
        _tetraminoController.SpawnTetramino();
    }

    public void LocalDisable()
    {
       
    }
}
