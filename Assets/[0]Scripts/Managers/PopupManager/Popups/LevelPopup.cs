using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : BasePopup
{
   [SerializeField] private Text _scoresText;
   [SerializeField] private Text _bestText;
   
   [SerializeField] private Image _nextTetraminoImage;
   [SerializeField] private RectTransform _gameOverPanel;

   private User _user;

   protected override void OnShow(object obj = null)
   {
      _user = InjectBox.Get<GameManager>().GetCurrentUser();
      
      UpdateScoresText(((int)obj).ToString());
      SetActiveBestText();
      
      EventManager.Subscribe<OnTetraminoSpawnEvent>(OnTetraminoSpawn);
      EventManager.Subscribe<OnScoreGainedEvent>(OnScoreGained);
      EventManager.Subscribe<OnGameOverEvent>(OnGameOver);
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
   
   public void UpdateNextTetraminoSprite(Sprite sprite)
   {
      _nextTetraminoImage.sprite = sprite;
   }

   public void OnClickMenuButton()
   {
      StageManager.LoadStage(StageID.Menu);
   }
   
   public void OnClickRetryButton()
   {
      SetActiveGameOverPanel(false);
      SetActiveBestText();
      EventManager.TriggerEvent<OnRetryLevelEvent>();
   }

}
