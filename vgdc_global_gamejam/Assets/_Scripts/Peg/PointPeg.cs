using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class PointPeg : BasicPeg
{
    //SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
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
        // float diff = .35f;
        // float og = .3;
        // sr.transform.localScale = new Vector3(diff, diff, 1);
        // yield return new WaitForSeconds(0.1);
        // sr.transform.localScale = new Vector3(og, og, 1);
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
