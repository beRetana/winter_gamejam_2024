using UnityEngine;
using UnityEngine.UIElements;

public class TurretTrajectoryLine : MonoBehaviour
{
    [SerializeField] private int _segmentCount = 50; // Number of points
    [SerializeField] private float _curveLength = 0.5f;

    [SerializeField] private Transform _ballSpawnPoint;

    private Vector2[] _segments;

    private LineRenderer _lineRenderer;

    private float _projectileSpeed;
    private float _projectileGravity;
    private float _projectileMass;

    private const float TIME_CURVE_ADDITION = 0.5f;

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.BallInfoUpdated, UpdatePhysicsInfo);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.BallInfoUpdated, UpdatePhysicsInfo);
    }
    private void Start()
    {
        _segments = new Vector2[_segmentCount];

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount;
    }
    private void FixedUpdate()
    {
        // Set starting position
        Vector2 startPos = _ballSpawnPoint.position;
        _segments[0] = startPos;

        _lineRenderer.SetPosition(0, startPos);

        // Physics calculations
        Vector2 startVelocity = _projectileSpeed * DataMessenger.GetVector2(Vector2Key.BulletDirection);

        for (int i = 0; i < _segmentCount; ++i)
        {
            float timeOffset = (i * Time.fixedDeltaTime * _curveLength);

            Vector2 gravityOffset = TIME_CURVE_ADDITION * _projectileGravity * Mathf.Pow(timeOffset, 2) *
                _projectileMass * Physics2D.gravity;

            _segments[i] = _segments[0] + startVelocity * timeOffset + gravityOffset;
            _lineRenderer.SetPosition(i, _segments[i]);
        }
    }
    private void UpdatePhysicsInfo()
    {
        _projectileSpeed = DataMessenger.GetFloat(FloatKey.CurrentBallSpeed);
        _projectileGravity = DataMessenger.GetFloat(FloatKey.CurrentBallGravityScale);
        _projectileMass = DataMessenger.GetFloat(FloatKey.CurrentBallMass);
    }
}
