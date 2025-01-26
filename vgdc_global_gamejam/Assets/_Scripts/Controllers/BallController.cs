using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float MIN_Y = -6;

    private Rigidbody2D _rigidbody;

    [SerializeField] private float _velocity;

    private float originalMass;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        originalMass = _rigidbody.mass;

        EventMessenger.StartListening(EventKey.UpdateBallDebuffs, UpdateDebuffs);
        
        UpdateDebuffs();
    }
    private void OnDestroy()
    {
        EventMessenger.StopListening(EventKey.UpdateBallDebuffs, UpdateDebuffs);
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.DestroyBall, DestroyBall);
        EventMessenger.StartListening(EventKey.ShootBall, AddForce);
        DataMessenger.SetGameObject(GameObjectKey.PlayerBall, gameObject);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.DestroyBall, DestroyBall);
        EventMessenger.StopListening(EventKey.ShootBall, AddForce);
    }
    private void Start()
    {
        //AddForce();
    }
    private void Update()
    {
        if (transform.position.y < MIN_Y)
        {
            DestroyBall();
        }
    }
    private void UpdateBallInfo()
    {
        DataMessenger.SetFloat(FloatKey.CurrentBallSpeed, _velocity);
        DataMessenger.SetFloat(FloatKey.CurrentBallGravityScale, _rigidbody.gravityScale);
        DataMessenger.SetFloat(FloatKey.CurrentBallMass, _rigidbody.mass);
        EventMessenger.TriggerEvent(EventKey.BallInfoUpdated);
    }
    
    private void UpdateDebuffs()
    {
        _rigidbody.mass = originalMass * DataMessenger.GetFloat(FloatKey.BallMassMultiplier);
        UpdateBallInfo();
    }

    private void AddForce()
    {
        _rigidbody.AddForce(DataMessenger.GetVector2(Vector2Key.BulletDirection) * _velocity, ForceMode2D.Impulse);

        DataMessenger.SetBool(BoolKey.IsBallInPlay, true);
    }

    private void DestroyBall()
    {
        DataMessenger.SetBool(BoolKey.IsBallInPlay, false);

        EventMessenger.TriggerEvent(EventKey.DestroyPegs);
        EventMessenger.TriggerEvent(EventKey.RoundEnded);

        if (DataMessenger.GetInt(IntKey.RemainingBallCount) == 0 && 
            DataMessenger.GetInt(IntKey.PointPegCount) != DataMessenger.GetInt(IntKey.StartingPointPegCount))
        {
            EventMessenger.TriggerEvent(EventKey.LostGame);
        }
        
        Debug.Log("Lost Ball: " + DataMessenger.GetInt(IntKey.RemainingBallCount));

        AudioPlayer.PlaySound(SoundKey.BallPop);

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
