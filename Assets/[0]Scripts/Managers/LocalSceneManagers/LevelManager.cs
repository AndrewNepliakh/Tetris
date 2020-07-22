using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to handle all processes of level stage
/// From this class starts Level
/// </summary>

[CreateAssetMenu(fileName = "LevelManager", menuName = "Managers/LevelManager")]
public class LevelManager : BaseInjectable, IAwake, IStart, IDisable
{
    private GameManager _gameManager;
    private TetraminoController _tetraminoController;
    private PopupManager _popupManager;
    private PoolManager _poolManager;
    
    public static bool IsGameOver { get; set; }
    
    public void OnAwake()
    {
        _gameManager = InjectBox.Get<GameManager>();
        _poolManager = InjectBox.Get<PoolManager>();
        _popupManager = InjectBox.Get<PopupManager>();
        _poolManager.PreLoad<Cube>(TetraminoController.Width * TetraminoController.Height);
       _tetraminoController = new TetraminoController();
       
       EventManager.Subscribe<OnGameOverEvent>(OnGameOver);
       EventManager.Subscribe<OnRetryLevelEvent>(OnRetryLevel);
       EventManager.Subscribe<OnMenuEvent>(OnMenu);
    }

    public void OnStart()
    {
        _gameManager.GetCurrentUser().Load();
        _popupManager.ShowPopup<LevelPopup>(_gameManager.GetCurrentUser().Score);
        _tetraminoController.SpawnTetramino();
    }
    
    private void OnGameOver(OnGameOverEvent obj)
    {
        IsGameOver = true;
        _gameManager.GetCurrentUser().Save();
    }
    
    private void OnRetryLevel(OnRetryLevelEvent obj)
    {
        _gameManager.GetCurrentUser().ResetScore();
        IsGameOver = false;
        _tetraminoController.ClearGrid();
        foreach (var cube in FindObjectsOfType<Cube>()) _poolManager.GetPool<Cube>().Deactivate(cube);
        _tetraminoController.SpawnTetramino();
    }
    
    private void OnMenu(OnMenuEvent obj)
    {
        _gameManager.GetCurrentUser().ResetScore();
        IsGameOver = false;
        StageManager.LoadStage(StageID.Menu);
    }

    public void LocalDisable()
    {
       
    }
}
