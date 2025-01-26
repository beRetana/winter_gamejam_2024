using System.Collections;
using UnityEngine;

public class CurseOfRa : ImageUtil
{
    private const float FADE_DURATION = 1;
    protected override void StartListeningToEvents()
    {
        base.StartListeningToEvents();

        EventMessenger.StartListening(EventKey.FlashCurseOfRa, Flash);
    }
    protected override void StopListeningToEvents()
    {
        base.StopListeningToEvents();

        EventMessenger.StopListening(EventKey.FlashCurseOfRa, Flash);
    }
    private void Flash()
    {
        image.enabled = true;
        image.SetAlpha(1);

        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        float fadeTimer = 0;
        while (fadeTimer <= FADE_DURATION)
        {
            image.SetAlpha(1 - fadeTimer / FADE_DURATION);
            fadeTimer += Time.deltaTime;
            yield return null;
        }
        image.enabled = false;
    }
}
