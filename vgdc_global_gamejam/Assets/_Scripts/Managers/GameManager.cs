using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
        EventMessenger.StartListening(EventKey.WonGame, WonGame);
        EventMessenger.StartListening(EventKey.LostGame, LostGame);
    }

    public void LostGame()
    {
        Debug.Log("Lost Game - From GameManager");
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        // Lost Game
    }
    
    public void WonGame()
    {
        Debug.Log("Won Game - From GameManager");
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        // Won Game UI???
    }
}
