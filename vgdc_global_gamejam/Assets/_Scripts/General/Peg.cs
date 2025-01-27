using UnityEngine;

public class Peg : MonoBehaviour
{
    private static int ID = 0;

    private int pegId;

    [SerializeField] private int scoreToGive;

    private bool hasBeenHit = false; // Whether this peg has been hit in the current round (ball)
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.RoundEnded, RoundEnded);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.RoundEnded, RoundEnded);
    }
    private void Start()
    {
        pegId = ++ID;

        DataMessenger.SetInt(IntKey.NewBluePegID, pegId);
        DataMessenger.SetGameObject(GameObjectKey.NewBluePegObject, gameObject);
        EventMessenger.TriggerEvent(EventKey.NewBluePegCreated);
    }
    private void Hit()
    {
        DataMessenger.SetInt(IntKey.ScoreToAdd, scoreToGive);
        EventMessenger.TriggerEvent(EventKey.AddScore);

        hasBeenHit = true;
    }
    private void RoundEnded()
    {
        if (hasBeenHit)
        {
            DestroyPeg();
        }
    }
    private void DestroyPeg()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Hit();
        }
    }
}
