using UnityEngine.SceneManagement;

/// <summary>
/// Class that represent menu screen
/// </summary>
public class MenuPopup : BasePopup
{
   public void OnClickNewGameButton()
   {
      StageManager.LoadStage(StageID.Level);
   }
   
   public void OnClickContinueButton()
   {
      
   }
}
