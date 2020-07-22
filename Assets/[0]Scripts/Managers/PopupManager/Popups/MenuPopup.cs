using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that represent menu screen
/// </summary>
public class MenuPopup : BasePopup
{
   public void OnClickNewGameButton()
   {
      PlayerPrefs.DeleteAll();
      StageManager.LoadStage(StageID.Level);
   }
   
   public void OnClickContinueButton()
   {
      StageManager.LoadStage(StageID.Level);
   }
}
