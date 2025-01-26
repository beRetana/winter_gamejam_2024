using System;
using System.Collections;
using UnityEngine;

public class DebufBlacHole : MonoBehaviour, IDebuf
{
    private Vector3 _defaultScale;
    private float _timeOn;
    private float _totalTimeOn = 4;

    public void ApplyDebuff()
    {
        //
    }

    private void Update() 
    {
        if (_timeOn > 0)
        {
            float proportion = (_totalTimeOn - _timeOn) / _totalTimeOn;
            float scaleValue = Mathf.Lerp(1f, .6f, proportion);
            transform.localScale = new Vector3(scaleValue, 1f, 1f);
            _timeOn -= Time.deltaTime;
        }
    }

    public void DisableDebuff()
    {
        transform.localScale = _defaultScale;
    }

    public void EnableDebuff()
    {
        _defaultScale = transform.localScale;
        _timeOn = _totalTimeOn;
    }
}
