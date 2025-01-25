using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretGunFiring : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _velocity;

    PlayerControls _playerControls;
    Transform _spawner;

    void Awake()
    {
        _playerControls = new();
    }

    void Start()
    {
        int firstChild = 0;
        _spawner = transform.GetChild(firstChild).transform;

        _playerControls.Player.Shoot.Enable();
        _playerControls.Player.Shoot.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        DataMessenger.SetVector3(Vector3Key.BulletDirection, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
        GameObject ball = Instantiate(_bulletPrefab, _spawner.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>()?.AddForce(DataMessenger.GetVector3(Vector3Key.BulletDirection) * _velocity, ForceMode2D.Impulse);
    }
}
