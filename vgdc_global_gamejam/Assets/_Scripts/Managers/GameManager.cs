using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winUIPrefab;
    [SerializeField] private GameObject _loseUIPrefab;
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.WonGame, WonGame);
        EventMessenger.StartListening(EventKey.LostGame, LostGame);

        EventMessenger.StartListening(EventKey.RestartGame, RestartGame);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.WonGame, WonGame);
        EventMessenger.StopListening(EventKey.LostGame, LostGame);

        EventMessenger.StopListening(EventKey.RestartGame, RestartGame);
    }
    private void Start()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
    }

    private void RestartGame()
    {
        string gameSceneName = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(gameSceneName);
        SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
    }
    public void LostGame()
    {
        Debug.Log("Lost Game - From GameManager");
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        // Lost Game
        Instantiate(_loseUIPrefab);
    }
    
    public void WonGame()
    {
        Debug.Log("Won Game - From GameManager");
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        // Won Game UI???
        Instantiate(_winUIPrefab);
    }
}
