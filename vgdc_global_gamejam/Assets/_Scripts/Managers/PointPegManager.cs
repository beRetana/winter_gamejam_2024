using System;
using UnityEngine;

public class PointPegManager : MonoBehaviour
{
    [SerializeField] private int _pointPegMaxCount = 15;
    [SerializeField] private int _pointPegCurse = 3;

    private int _pointPegMultipliyer;

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.RoundEnded, OnPointPegCaught);
        EventMessenger.StartListening(EventKey.RestartGame, RestartGame);

    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.RoundEnded, OnPointPegCaught);
        EventMessenger.StopListening(EventKey.RestartGame, RestartGame);
    }
    private void RestartGame()
    {
        _pointPegMultipliyer = 0;
    }
    private void OnPointPegCaught()
    {
        int pointPegCount = DataMessenger.GetInt(IntKey.PointPegCount);
        _pointPegMultipliyer++;
        
        if (pointPegCount == 0){return;}

        if (pointPegCount == _pointPegMaxCount)
        {
            EventMessenger.TriggerEvent(EventKey.WonGame);
            return;
        }

        if (pointPegCount >= _pointPegCurse * _pointPegMultipliyer ) 
        {
            EventMessenger.TriggerEvent(EventKey.ShowDebuffSelection);
        }
    }
}
