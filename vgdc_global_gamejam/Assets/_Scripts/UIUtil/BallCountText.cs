using UnityEngine;

public class BallCountText : TMPUtil
{
    private const string TEXT_PREFIX = "Balls: ";
    protected override void StartListeningToEvents()
    {
        base.StartListeningToEvents();

        EventMessenger.StartListening(EventKey.BallCountUpdated, UpdateText);
    }
    protected override void StopListeningToEvents()
    {
        base.StopListeningToEvents();

        EventMessenger.StopListening(EventKey.BallCountUpdated, UpdateText);
    }
    protected override void UpdateText()
    {
        text.text = TEXT_PREFIX + DataMessenger.GetInt(IntKey.RemainingBallCount);
    }
}
