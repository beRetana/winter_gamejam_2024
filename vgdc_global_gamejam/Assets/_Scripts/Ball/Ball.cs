using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.01f;

    private Quaternion _targetRotation = Quaternion.Euler(0, 0, 360);
    
    void Update()
    {
        if (_targetRotation == Quaternion.identity)
        {
            _targetRotation = Quaternion.Euler(0, 0, 360);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.time * _rotationSpeed);
        Debug.Log("Ball Rotation: " + transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPeg peg))
        {
            int updatedScore = peg.CalculateScore(DataMessenger.GetInt(IntKey.CurrentScore));
            
            // Apply effect should get called after the score is updated.
            peg.ApplyEffect();

            DataMessenger.SetInt(IntKey.CurrentScore, updatedScore);
            EventMessenger.TriggerEvent(EventKey.ScoreUpdated);
        }
    }
}
