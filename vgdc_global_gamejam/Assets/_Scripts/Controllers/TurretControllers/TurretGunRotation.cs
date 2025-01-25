using UnityEngine;

public class TurretGunRotation : MonoBehaviour
{
    private Transform gunAxis;
    
    private void Start()
    {
        //gunAxis = transform.parent;
    }
    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //gunAxis.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 180);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 180);
    }
}
