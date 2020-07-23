using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : BasePopup
{
   [SerializeField] private Text _scoresText;
   [SerializeField] private Text _bestText;
   [SerializeField] private Text _speedText;
   
   [SerializeField] private Image _nextTetraminoImage;
   [SerializeField] private RectTransform _gameOverPanel;

   private User _user;
   private LevelManager _levelManager;

   private int _speed;

   protected override void OnShow(object obj = null)
   {
      _user = InjectBox.Get<GameManager>().GetCurrentUser();
      _levelManager = InjectBox.Get<LevelManager>();
      _speed = 1;
      
      UpdateScoresText(((int)obj).ToString());
      UpdateSpeedText(_speed.ToString());
      SetActiveBestText();
      
      EventManager.Subscribe<OnTetraminoSpawnEvent>(OnTetraminoSpawn);
      EventManager.Subscribe<OnScoreGainedEvent>(OnScoreGained);
      EventManager.Subscribe<OnGameOverEvent>(OnGameOver);
      EventManager.Subscribe<OnIncreasedSpeedEvent>(OnIncreasedSpeed);
   }
   private void OnGameOver(OnGameOverEvent obj)
   {
      SetActiveGameOverPanel(true);
   }

   private void OnScoreGained(OnScoreGainedEvent obj)
   {
      UpdateScoresText(InjectBox.Get<GameManager>().GetCurrentUser().AddScore().ToString());
   }

   private void OnTetraminoSpawn(OnTetraminoSpawnEvent obj)
   {
      UpdateNextTetraminoSprite(InjectBox.Get<TetraminoData>().GetIcon(obj.TetraminoId));
   }
   
   private void OnIncreasedSpeed(OnIncreasedSpeedEvent obj)
   {
      UpdateSpeedText((++_speed).ToString());
   }

   private void SetActiveGameOverPanel(bool condition)
   {
      _gameOverPanel.gameObject.SetActive(condition);
   }
   
   private void SetActiveBestText()
   {
      if (_user.BestScore > 0)
      {
         UpdateBestText(_user.BestScore.ToString());
         _bestText.gameObject.SetActive(true);
      }
   }
   
   public void UpdateScoresText(string value)
   {
      _scoresText.text = "scores: " + value;
   }
   
   public void UpdateBestText(string value)
   {
      _bestText.text = "best: " + value;
   }

   public void UpdateSpeedText(string value)
   {
      _speedText.text = "speed: " + value;
   }

   public void UpdateNextTetraminoSprite(Sprite sprite)
   {
      _nextTetraminoImage.sprite = sprite;
   }

   public void OnClickMenuButton()
   {
      EventManager.TriggerEvent<OnMenuEvent>();
   }
   
   
   public void OnClickRetryButton()
   {
      EventManager.TriggerEvent<OnRetryLevelEvent>();
      SetActiveGameOverPanel(false);
      SetActiveBestText();
      UpdateScoresText(_user.Score.ToString());
      _speed = 1;
      UpdateSpeedText(_speed.ToString());
   }

}
