using UnityEngine;

public class BallBackHoleTarget : MonoBehaviour
{
    private const string BALL_TAG = "Ball";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(BALL_TAG))
        {
            EventMessenger.TriggerEvent(EventKey.AddBall);
            EventMessenger.TriggerEvent(EventKey.DestroyBall);
        }
    }
}
