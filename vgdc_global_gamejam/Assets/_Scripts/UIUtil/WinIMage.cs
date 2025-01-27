using UnityEngine;
using System.Collections;

public class WinIMage : ImageUtil
{
    [SerializeField] Sprite winSprite;
    [SerializeField] Sprite loseSprite;
    protected override void StartListeningToEvents()
    {
        base.StartListeningToEvents();

        EventMessenger.StartListening(EventKey.WonGame, ShowW);
        EventMessenger.StartListening(EventKey.LostGame, ShowL);
        EventMessenger.StartListening(EventKey.RestartGame, Hide);

    }
    protected override void StopListeningToEvents()
    {
        base.StopListeningToEvents();

        EventMessenger.StopListening(EventKey.WonGame, ShowW);
        EventMessenger.StartListening(EventKey.LostGame, ShowL);
        EventMessenger.StartListening(EventKey.RestartGame, Hide);
    }

    private void ShowW()
    {
        image.sprite = winSprite;
        image.enabled = true;
        image.SetAlpha(1);
    }

    private void ShowL()
    {
        image.sprite = loseSprite;
        image.enabled = true;
        image.SetAlpha(1);
    }

    private void Hide()
    {
        image.enabled = false;
        image.SetAlpha(0);
    }

}
