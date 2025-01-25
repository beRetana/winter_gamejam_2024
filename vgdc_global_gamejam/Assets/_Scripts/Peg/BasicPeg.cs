using System.Collections;
using UnityEngine;

public class BasicPeg : MonoBehaviour, IPeg
{
    [SerializeField] private int _scoredAdded;
    [SerializeField] private float _timeOn;

    private bool _hasBeenHit;

    void Start(){
        EventMessenger.StartListening(EventKey.RoundEnded, OnDestroyPeg);
    }

    public void ApplyEffect()
    {
        
    }

    public int CalculateScore(int score)
    {
        if (!_hasBeenHit){
            _hasBeenHit = true;
            return score + _scoredAdded;
        }
        return score;
    }

    private void ApplyPopEffect()
    {
        // Animation and sound effects
    }

    public void OnDestroyPeg()
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
