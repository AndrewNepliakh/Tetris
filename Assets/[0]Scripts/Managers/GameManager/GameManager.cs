using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Managers/GameManager")]
public sealed class GameManager : BaseInjectable
{
    private UserManager _userManager; 
        
    private User _currentUser;

    public void Initialize()
    {
        _currentUser = new User();
    }

    public User GetCurrentUser() => _currentUser;


}