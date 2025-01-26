using System.Collections;
using Codice.CM.Common;
using UnityEngine;

public class BasicPeg : MonoBehaviour, IPeg
{
    [SerializeField] protected int _scoredAdded;
    [SerializeField] protected float _timeOn;


    protected bool _hasBeenHit;
    protected bool _effecthasBeenApplied;
    protected IDebuf _debuf;

    void Start(){
        EventMessenger.StartListening(EventKey.RoundEnded, OnDestroyPeg);
    }

    public virtual void ApplyEffect()
    {
        
    }

    public void ApplyDebuf(IDebuf debuf)
    {
        _debuf?.DisableDebuff();
        _debuf = debuf;
        _debuf?.EnableDebuff();
    }

    public void DisableDebuff()
    {
        _debuf?.DisableDebuff();
    }

    public virtual int CalculateScore(int score)
    {
        if (!_hasBeenHit){
            _hasBeenHit = true;
            _debuf?.ApplyDebuff();
            return score + _scoredAdded;
        }
        return score;
    }

    private void ApplyPopEffect()
    {
        // Animation and sound effects
    }

    public virtual void OnDestroyPeg()
    {
        if (_hasBeenHit)
        {
            ApplyPopEffect();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        StartCoroutine(TimeOn());
    }

    void OnCollisionExit2D(Collision2D other)
    {
        StopCoroutine(TimeOn());
    }

    IEnumerator TimeOn()
    {
        yield return new WaitForSecondsRealtime(_timeOn);
        _hasBeenHit = true;
        OnDestroyPeg();
    }
}
