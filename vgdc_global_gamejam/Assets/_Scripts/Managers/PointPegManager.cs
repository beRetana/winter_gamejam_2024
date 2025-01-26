using System;
using UnityEngine;

public class PointPegManager : MonoBehaviour
{
    [SerializeField] private int _pointPegMaxCount = 15;
    [SerializeField] private int _pointPegCurse = 3;

    private int _pointPegMultipliyer;

    void Start()
    {
        EventMessenger.StartListening(EventKey.RoundEnded, OnPointPegCaught);
    }

    private void OnPointPegCaught()
    {
        int pointPegCount = DataMessenger.GetInt(IntKey.PointPegCount);
        _pointPegMultipliyer++;
        
        if (pointPegCount == 0){return;}

        if (pointPegCount == _pointPegMaxCount)
        {
            EventMessenger.TriggerEvent(EventKey.WonGame);
        }

        if (pointPegCount >= _pointPegCurse * _pointPegMultipliyer ) 
        {
            EventMessenger.TriggerEvent(EventKey.ShowDebuffSelection);
        }
    }
}
