using System;
using UnityEngine;

public class PointPegManager : MonoBehaviour
{
    [SerializeField] private int _pointPegMaxCount = 15;

    void Start()
    {
        EventMessenger.StartListening(EventKey.PointPegCaught, CheckWonGame);
    }

    private void CheckWonGame()
    {
        int poinPegCount = DataMessenger.GetInt(IntKey.PointPegCount);

        if (poinPegCount == _pointPegMaxCount)
        {
            Debug.Log("Won Game");
            EventMessenger.TriggerEvent(EventKey.WonGame);
        }
    }
}
