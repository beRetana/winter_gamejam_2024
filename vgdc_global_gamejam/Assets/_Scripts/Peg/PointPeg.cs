using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class PointPeg : BasicPeg
{
    public override void OnDestroyPeg()
    {
        if (_hasBeenHit)
        {
            ApplyPopEffect();
            //
            Destroy(gameObject);
        }
    }

    private void UpdatePointPegCount()
    {
        DataMessenger.SetInt(IntKey.PointPegCount, DataMessenger.GetInt(IntKey.PointPegCount) + 1);
        Debug.Log("Point Peg Count: " + DataMessenger.GetInt(IntKey.PointPegCount));
        EventMessenger.TriggerEvent(EventKey.PointPegCaught);
    }

    private void ApplyPopEffect()
    {
        // Animation and sound effects
    }

    public override void ApplyEffect()
    {
        if (!_effecthasBeenApplied)
        {
            _effecthasBeenApplied = true;
            UpdatePointPegCount();
        }
    }
}
