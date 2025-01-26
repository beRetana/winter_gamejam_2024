using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class PointPeg : BasicPeg
{
    private static int _pointPeg_ID;

    public override void OnDestroyPeg()
    {
        if (_hasBeenHit)
        {
            ApplyPopEffect();
            //
            EventMessenger.TriggerEvent(EventKey.DestroyedPointPeg);
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        EventMessenger.StartListening(EventKey.RoundEnded, OnDestroyPeg);

        _pointPeg_ID++;
        _myId = _pointPeg_ID;

        _animator = GetComponent<Animator>();

        DataMessenger.SetInt(IntKey.NewPointPegID, _myId);
        DataMessenger.SetGameObject(GameObjectKey.NewPointPegObject, gameObject);
        EventMessenger.TriggerEvent(EventKey.NewPointPegCreated);
    }

    private void OnDestroy()
    {
        if (_hasBeenHit)
        {
            ApplyPopEffect();
            DataMessenger.SetInt(IntKey.DestroyedPointPegID, _myId);
            EventMessenger.TriggerEvent(EventKey.DestroyedPointPeg);
            Destroy(gameObject);
        }
    }

    private void UpdatePointPegCount()
    {
        DataMessenger.SetInt(IntKey.PointPegCount, DataMessenger.GetInt(IntKey.PointPegCount) + 1);
        EventMessenger.TriggerEvent(EventKey.PointPegCaught);
    }

    private void ApplyPopEffect()
    {
        
    }

    public override void ApplyEffect()
    {
        if (!_effecthasBeenApplied)
        {
            _effecthasBeenApplied = true;

            GetComponentInChildren<SpriteRenderer>().sprite = _interactedSprite;
            _animator.SetBool(_HAS_BEEN_HIT, true);

            UpdatePointPegCount();
        }
    }
}
