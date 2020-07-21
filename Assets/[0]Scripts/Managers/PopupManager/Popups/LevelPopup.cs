using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : BasePopup
{
   [SerializeField] private Text _scoresText;
   [SerializeField] private Image _nextTetraminoImage;
   [SerializeField] private RectTransform _gameOverPanel;

   protected override void OnShow(object obj = null)
   {
      UpdateScoresText(((int)obj).ToString());
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
   
   public void UpdateScoresText(string value)
   {
      _scoresText.text = "scores: " + value;
   }
   
   public void UpdateNextTetraminoSprite(Sprite sprite)
   {
      _nextTetraminoImage.sprite = sprite;
   }

   private void SetActiveGameOverPanel(bool condition)
   {
      _gameOverPanel.gameObject.SetActive(condition);
   }
}
