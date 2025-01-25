using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
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
