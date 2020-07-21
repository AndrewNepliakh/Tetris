using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to handle all processes of level stage
/// </summary>

[CreateAssetMenu(fileName = "LevelManager", menuName = "Managers/LevelManager")]
public class LevelManager : BaseInjectable, IAwake, IStart, IDisable
{
    private GameManager _gameManager;
    private TetraminoController _tetraminoController;
    private PopupManager _popupManager;
    private PoolManager _poolManager;
    
    public void OnAwake()
    {
        _gameManager = InjectBox.Get<GameManager>();
        _poolManager = InjectBox.Get<PoolManager>();
        _popupManager = InjectBox.Get<PopupManager>();
        _poolManager.PreLoad<Cube>((int)(TetraminoController.Width * TetraminoController.Height));
       _tetraminoController = new TetraminoController();
    }
    
    public void OnStart()
    {
        _popupManager.ShowPopup<LevelPopup>(_gameManager.GetCurrentUser().Scores);
        _tetraminoController.SpawnTetramino();
    }

    public void LocalDisable()
    {
       
    }
}
