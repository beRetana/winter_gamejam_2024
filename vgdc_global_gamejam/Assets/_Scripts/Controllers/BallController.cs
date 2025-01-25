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

        UpdateDebuffs();
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.DestroyBall, DestroyBall);
        EventMessenger.StartListening(EventKey.ShootBall, AddForce);
        EventMessenger.StartListening(EventKey.UpdateBallDebuffs, UpdateDebuffs);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.DestroyBall, DestroyBall);
        EventMessenger.StopListening(EventKey.ShootBall, AddForce);
        EventMessenger.StopListening(EventKey.UpdateBallDebuffs, UpdateDebuffs);
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

        EventMessenger.TriggerEvent(EventKey.RoundEnded);

        Destroy(gameObject);
    }
}
