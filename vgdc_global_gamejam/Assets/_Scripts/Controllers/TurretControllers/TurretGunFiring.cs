using UnityEngine;

public class TurretGunFiring : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        DataMessenger.SetVector3(Vector3Key.BulletDirection, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
        Instantiate(bulletPrefab);
    }
}
