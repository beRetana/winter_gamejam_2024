using System;
using System.Collections;
using UnityEngine;

public class DebufBlacHole : MonoBehaviour, IDebuf
{
    private GameObject _blackHole;
    private Vector3 _defaultScale;
    private float _lerpValue;

    public void ApplyDebuff()
    {
        //
    }

    private void Start() {
        EnableDebuff();
    }

    private void Update() 
    {
        if (_lerpValue > 0)
        {
            _lerpValue -= Time.deltaTime;
            _lerpValue = Mathf.Lerp(_lerpValue, .6f, Time.deltaTime);
            _blackHole.transform.localScale = new Vector3(_lerpValue, 1f, 1f);
        }
    }

    public void DisableDebuff()
    {
        _blackHole.transform.localScale = _defaultScale;
    }

    public void EnableDebuff()
    {
        _blackHole = GameObject.FindGameObjectWithTag("Turret");
        _defaultScale = _blackHole.transform.localScale;
        _lerpValue = 1;
    }
}
