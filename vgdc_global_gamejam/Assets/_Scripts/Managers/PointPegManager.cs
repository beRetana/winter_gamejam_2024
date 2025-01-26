using System;
using UnityEngine;

public class PointPegManager : MonoBehaviour
{
    [SerializeField] private int _pointPegMaxCount = 15;
    [SerializeField] private int _pointPegCurseInterval = 3;

    private int _pointPegMultipliyer;

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.RoundEnded, OnPointPegCaught);
        EventMessenger.StartListening(EventKey.RestartGame, ResetInfo);

    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.RoundEnded, OnPointPegCaught);
        EventMessenger.StopListening(EventKey.RestartGame, ResetInfo);
    }
    private void Start()
    {
        DataMessenger.SetInt(IntKey.StartingPointPegCount, _pointPegMaxCount);

        ResetInfo();
    }
    private void ResetInfo()
    {
        _pointPegMultipliyer = 1;
    }
    private void OnPointPegCaught()
    {
        int pointPegCount = DataMessenger.GetInt(IntKey.PointPegCount);
        
        if (pointPegCount == 0){return;}

        if (pointPegCount == _pointPegMaxCount)
        {
            EventMessenger.TriggerEvent(EventKey.WonGame);
            return;
        }
        if (pointPegCount >= _pointPegCurseInterval * _pointPegMultipliyer && DataMessenger.GetBool(BoolKey.IsGameActive)) 
        {
            EventMessenger.TriggerEvent(EventKey.ShowDebuffSelection);
            _pointPegMultipliyer = pointPegCount / _pointPegCurseInterval + 1;
        }
    }
}
