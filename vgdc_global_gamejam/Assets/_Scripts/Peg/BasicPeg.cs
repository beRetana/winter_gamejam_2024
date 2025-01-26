using System.Collections;
using System.Numerics;
using UnityEngine;

public class BasicPeg : MonoBehaviour, IPeg
{
    [SerializeField] protected int _scoredAdded;
    [SerializeField] protected float _timeOn;
    [SerializeField] protected Sprite _interactedSprite;

    private static int ID;

    protected Animator _animator;
    protected int _myId;
    protected string _HAS_BEEN_HIT = "HasBeenHit";

    protected virtual void Start()
    {
        EventMessenger.StartListening(EventKey.RoundEnded, OnDestroyPeg);

        ID++;

        _myId = ID;

        _animator = GetComponent<Animator>();

        DataMessenger.SetInt(IntKey.NewBluePegID, _myId);
        DataMessenger.SetGameObject(GameObjectKey.NewBluePegObject, gameObject);
        EventMessenger.TriggerEvent(EventKey.NewBluePegCreated);
    }

    protected bool _hasBeenHit;
    protected bool _effecthasBeenApplied;
    protected IDebuf _debuf;

    public virtual void ApplyEffect()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = _interactedSprite;
        _animator.SetBool(_HAS_BEEN_HIT, true);

        AudioPlayer.PlaySound(SoundKey.BubblePop, 0.95f, 1.05f);
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
