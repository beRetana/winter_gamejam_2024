using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winUIPrefab;
    [SerializeField] private GameObject _loseUIPrefab;

    [SerializeField] private string _sceneToLoad; // Scene to load in build
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

        if (!Application.isEditor)
        {
            SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        }
    }

    private void RestartGame()
    {
        DataMessenger.SetInt(IntKey.PointPegCount, 0);
        DataMessenger.SetBool(BoolKey.IsGameActive, true);

        // Reset score
        DataMessenger.SetInt(IntKey.ScoreToAdd, -DataMessenger.GetInt(IntKey.CurrentScore));
        EventMessenger.TriggerEvent(EventKey.AddScore);

        EventMessenger.TriggerEvent(EventKey.ResetDebuffs);

        string gameSceneName = SceneManager.GetActiveScene().name;

        SceneManager.UnloadSceneAsync(gameSceneName);
        SceneManager.LoadSceneAsync(gameSceneName);
    }
    
    public void LostGame()
    {
        Debug.Log("Lost Game - From GameManager");
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        Time.timeScale = 0f;
        // Lost Game
        Instantiate(_loseUIPrefab);
    }
    
    public void WonGame()
    {
        Debug.Log("Won Game - From GameManager");
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        Time.timeScale = 0f;
        // Won Game UI???
        Instantiate(_winUIPrefab);
    }
}
