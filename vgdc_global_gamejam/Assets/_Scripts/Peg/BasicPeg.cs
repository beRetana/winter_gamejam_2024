using System.Collections;
using UnityEngine;

public class BasicPeg : MonoBehaviour, IPeg
{
    [SerializeField] protected int _scoredAdded;
    [SerializeField] protected float _timeOn;

    private static int ID;

    protected int _myId;

    protected virtual void Start()
    {
        EventMessenger.StartListening(EventKey.RoundEnded, OnDestroyPeg);

        ID++;

        _myId = ID;

        DataMessenger.SetInt(IntKey.NewBluePegID, _myId);
        DataMessenger.SetGameObject(GameObjectKey.NewBluePegObject, gameObject);
        EventMessenger.TriggerEvent(EventKey.NewBluePegCreated);
    }

    protected bool _hasBeenHit;
    protected bool _effecthasBeenApplied;
    protected IDebuf _debuf;

    public virtual void ApplyEffect()
    {
        
    }

    public void ApplyDebuf()
    {
        _debuf?.DisableDebuff();
        _debuf = GetComponent<IDebuf>();
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
            DataMessenger.SetInt(IntKey.DestroyedBluePegID, _myId);
            EventMessenger.TriggerEvent(EventKey.DestroyedBluePeg);
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
