using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretGunFiring : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    private PlayerControls _playerControls;
    private Transform _spawner;

    [SerializeField] private int startingBalls;
    private int remainingBalls;

    void Awake()
    {
        _playerControls = new();
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.AddBall, AddBall);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.AddBall, AddBall);
    }

    void Start()
    {
        int firstChild = 0;
        _spawner = transform.GetChild(firstChild).transform;

        _playerControls.Player.Shoot.Enable();
        _playerControls.Player.Shoot.performed += Shoot;

        remainingBalls = startingBalls;

        UpdateBallCount(0);
    }
    private void AddBall()
    {
        UpdateBallCount(1);
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        if (DataMessenger.GetBool(BoolKey.IsBallInPlay) || remainingBalls <= 0)
        {
            return;
        }
        DataMessenger.SetVector3(Vector3Key.BulletDirection, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
        Instantiate(_bulletPrefab, _spawner.transform.position, Quaternion.identity);

        UpdateBallCount(-1);
    }


    /// <param name="operand">1 to add, -1 to remove.</param>
    private void UpdateBallCount(int operand)
    {
        remainingBalls += operand;

        DataMessenger.SetInt(IntKey.RemainingBallCount, remainingBalls);
        EventMessenger.TriggerEvent(EventKey.BallCountUpdated);
    }
}
