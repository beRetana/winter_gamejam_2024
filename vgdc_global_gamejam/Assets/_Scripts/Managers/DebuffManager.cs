using System;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    private const float DEBUFF_PEG_CHANCE = 0.6f;
    private const float DEBUFF_BOARD_CHANCE = 0.8f;
    private const float DEBUFF_SHOT_CHANCE = 1f;
    public enum DebuffType
    {
        Peg, Board, Shot
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.ApplyDebuff, ApplyDebuff);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.ApplyDebuff, ApplyDebuff);
    }
    private void ApplyDebuff()
    {
        // Choose top-level debuff type
        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
        DebuffType debuffType = DebuffType.Peg;
        if (rand > DEBUFF_SHOT_CHANCE)
        {
            debuffType = DebuffType.Shot;
        }
        else if (rand > DEBUFF_BOARD_CHANCE)
        {
            debuffType = DebuffType.Board;
        }

        switch (debuffType)
        {
            case DebuffType.Peg:
                EventMessenger.TriggerEvent(EventKey.ApplyPegDebuff);
                break;
            case DebuffType.Board:
                EventMessenger.TriggerEvent(EventKey.ApplyBoardDebuff);
                break;
            case DebuffType.Shot:
                break;
        }
    }
}
