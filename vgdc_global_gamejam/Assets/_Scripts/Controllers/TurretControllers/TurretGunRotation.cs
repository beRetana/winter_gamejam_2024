using UnityEngine;

public class TurretGunRotation : MonoBehaviour
{
    
    private float _MIN_ANGLE = 285;
    private float _MAX_ANGLE = 75;

    private void Awake()
    {
        DataMessenger.SetVector2(Vector2Key.TurretPosition, transform.position);
    }
    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float newRotationZ = (Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 180)).eulerAngles.z;

        if (newRotationZ >= _MIN_ANGLE || newRotationZ <= _MAX_ANGLE)
        {
            transform.rotation = Quaternion.Euler(0, 0, newRotationZ);
        }
    }
}
