using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
/// <summary>
/// Class to handle all processes of level stage
/// From this class starts Level
/// </summary>

[CreateAssetMenu(fileName = "LevelManager", menuName = "Managers/LevelManager")]
public class LevelManager : BaseInjectable, IAwake, IStart, IDisable
{
    private const float DEFAULT_SPEED = 0.8f;
    
    private User _user;
    private TetraminoController _tetraminoController;
    private PopupManager _popupManager;
    private PoolManager _poolManager;

    private static float _speed = DEFAULT_SPEED;
    
    public static bool IsGameOver { get; set; }
    public static float Speed => _speed;
    
    public void OnAwake()
    {
        _user = InjectBox.Get<GameManager>().GetCurrentUser();
        _poolManager = InjectBox.Get<PoolManager>();
        _popupManager = InjectBox.Get<PopupManager>();
        _poolManager.PreLoad<Cube>(TetraminoController.Width * TetraminoController.Height);
       _tetraminoController = new TetraminoController();
       
       EventManager.Subscribe<OnGameOverEvent>(OnGameOver);
       EventManager.Subscribe<OnRetryLevelEvent>(OnRetryLevel);
       EventManager.Subscribe<OnMenuEvent>(OnMenu);
       EventManager.Subscribe<OnCompleteLineEvent>(OnCompleteLine);
    }

    public void OnStart()
    {
        _user.Load();
        _popupManager.ShowPopup<LevelPopup>(_user.Score);
        _tetraminoController.SpawnTetramino();
    }
    
    private void OnGameOver(OnGameOverEvent obj)
    {
        IsGameOver = true;
        _user.Save();
    }
    
    private void OnRetryLevel(OnRetryLevelEvent obj)
    {
        _user.ResetScore();
        IsGameOver = false;
        _speed = DEFAULT_SPEED;
        _tetraminoController.ClearGrid();
        foreach (var cube in FindObjectsOfType<Cube>()) _poolManager.GetPool<Cube>().Deactivate(cube);
        _tetraminoController.SpawnTetramino();
    }
    
    private void OnMenu(OnMenuEvent obj)
    {
        _user.ResetScore();
        IsGameOver = false;
        StageManager.LoadStage(StageID.Menu);
    }
    
    public void OnCompleteLine(OnCompleteLineEvent obj)
    {
        if (_user.Score % 20 == 0 && _speed > 0.2f)
        {
            _speed -= 0.1f;
            EventManager.TriggerEvent<OnIncreasedSpeedEvent>();
        }
    }

    public void LocalDisable()
    {
       
    }
}
