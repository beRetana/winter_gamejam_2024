using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretGunFiring : MonoBehaviour
{
    private const string SPAWN_POINT_OBJECT_NAME = "Spawner";

    [SerializeField] private GameObject _bulletPrefab;

    private PlayerControls _playerControls;
    private Transform _spawner;

    [SerializeField] private int _startingBalls;
    private int _remainingBalls;

    private Transform _currentBallTransform;

    void Awake()
    {
        _playerControls = new();
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.AddBall, AddBall);
        EventMessenger.StartListening(EventKey.RoundEnded, CreateBall);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.AddBall, AddBall);
        EventMessenger.StopListening(EventKey.RoundEnded, CreateBall);
    }

    void Start()
    {
        _spawner = transform.Find(SPAWN_POINT_OBJECT_NAME);

        _playerControls.Player.Shoot.Enable();
        _playerControls.Player.Shoot.performed += Shoot;

        _remainingBalls = _startingBalls;

        UpdateBallCount(0);

        CreateBall();
    }
    private void Update()
    {
        DataMessenger.SetVector2(Vector2Key.BulletDirection, 
            (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
    }

    private void AddBall()
    {
        UpdateBallCount(1);
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        if (_currentBallTransform == null || !DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            return;
        }

        _currentBallTransform.position = _spawner.transform.position;
        _currentBallTransform.gameObject.SetActive(true);
        _currentBallTransform = null;

        EventMessenger.TriggerEvent(EventKey.ShootBall);

        UpdateBallCount(-1);
    }

    /// <summary>
    /// Create ball but don't shoot yet.
    /// </summary>
    private void CreateBall()
    {
        if (DataMessenger.GetBool(BoolKey.IsBallInPlay) || _remainingBalls <= 0)
        {
            return;
        }
        _currentBallTransform = Instantiate(_bulletPrefab, _spawner.transform.position, Quaternion.identity).transform;
        _currentBallTransform.gameObject.SetActive(false);
    }

    /// <param name="operand">1 to add, -1 to remove.</param>
    private void UpdateBallCount(int operand)
    {
        _remainingBalls += operand;

        DataMessenger.SetInt(IntKey.RemainingBallCount, _remainingBalls);
        EventMessenger.TriggerEvent(EventKey.BallCountUpdated);
    }
}
