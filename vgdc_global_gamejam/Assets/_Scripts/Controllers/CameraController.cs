using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float tiltAngle = 30;
    [SerializeField] private float tiltSpeed = 10;
    private int tiltDirection;
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.TiltCamera, TiltCamera);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.TiltCamera, TiltCamera);
    }
    private void Start()
    {
        tiltDirection = Random.Range(0, 1) == 0 ? 1 : -1;
    }
    private void TiltCamera()
    {
        StartCoroutine(HandleTiltCamera());
    }
    private IEnumerator HandleTiltCamera()
    {
        float tilt = 0;
        while (tilt <= tiltAngle)
        {
            float tiltAmount = tiltSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 1), tiltAmount);
            tilt += tiltAmount;
            yield return null;
        }

        yield break;
    }
}
