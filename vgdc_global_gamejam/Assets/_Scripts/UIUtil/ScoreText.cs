using UnityEngine;

public class ScoreText : TMPUtil
{
    private const string TEXT_PREFIX = "Score: ";
    
    public override void Start()
    {
        base.Start();

        UpdateText();
    }

    protected override void UpdateText()
    {
        text.text = TEXT_PREFIX + DataMessenger.GetInt(IntKey.CurrentScore);
    }
    protected override void StartListeningToEvents()
    {
        base.StartListeningToEvents();

        EventMessenger.StartListening(EventKey.ScoreUpdated, UpdateText);
    }
    protected override void StopListeningToEvents()
    {
        base.StopListeningToEvents();

        EventMessenger.StopListening(EventKey.ScoreUpdated, UpdateText);
    }
    
}
