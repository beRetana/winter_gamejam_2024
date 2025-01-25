using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public enum BoardDebuffType
    {
        TiltBoard,
    }

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.ApplyBoardDebuff, ApplyDebuff);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.ApplyBoardDebuff, ApplyDebuff);
    }
    private void TiltBoard()
    {
        EventMessenger.TriggerEvent(EventKey.TiltCamera);
    }
    private void ApplyDebuff()
    {
        BoardDebuffType debuffType = (BoardDebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(BoardDebuffType)).Length);

        switch (debuffType)
        {
            case BoardDebuffType.TiltBoard:
                TiltBoard();
                break;
        }

        Debug.Log("Applied Board Debuff: " + debuffType);
    }
}
