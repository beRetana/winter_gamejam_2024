using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.AddScore, AddScore);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.AddScore, AddScore);
    }
    private void Start()
    {
        ResetScore();
    }
    private void AddScore()
    {
        DataMessenger.OperateInt(IntKey.CurrentScore, DataMessenger.GetInt(IntKey.ScoreToAdd));
        EventMessenger.TriggerEvent(EventKey.ScoreUpdated);
    }
    private void ResetScore()
    {
        DataMessenger.SetInt(IntKey.CurrentScore, 0);
    }
}
