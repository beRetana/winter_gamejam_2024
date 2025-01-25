using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float MIN_Y = -6;

    private Rigidbody2D rb;

    [SerializeField] private float _velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        AddForce();

        DataMessenger.SetBool(BoolKey.IsBallInPlay, true);
    }
    private void Update()
    {
        if (transform.position.y < MIN_Y)
        {
            DestroyBall();
        }
    }
    private void AddForce()
    {
        rb.AddForce(DataMessenger.GetVector3(Vector3Key.BulletDirection) * _velocity, ForceMode2D.Impulse);
    }
    private void DestroyBall()
    {
        DataMessenger.SetBool(BoolKey.IsBallInPlay, false);

        EventMessenger.TriggerEvent(EventKey.RoundEnded);

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IPeg>() != null)
        {
            int _currentScore = DataMessenger.GetInt(IntKey.CurrentScore);
            _currentScore = collision.gameObject.GetComponent<IPeg>().CalculateScore(_currentScore);
            DataMessenger.SetInt(IntKey.CurrentScore, _currentScore);
            EventMessenger.TriggerEvent(EventKey.ScoreUpdated);
            collision.gameObject.GetComponent<IPeg>().ApplyEffect();
        }
    }
}
