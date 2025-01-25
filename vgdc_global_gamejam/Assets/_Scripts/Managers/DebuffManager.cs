using System;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
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
        DebuffType debuffType = (DebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(DebuffType)).Length);

        switch (debuffType)
        {
            case DebuffType.Peg:
                EventMessenger.TriggerEvent(EventKey.ApplyPegDebug);
                break;
            case DebuffType.Board:
                break;
            case DebuffType.Shot:
                break;
        }
    }
}
