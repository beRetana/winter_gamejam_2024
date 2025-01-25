using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPeg peg))
        {
            peg.ApplyEffect();
            int updatedScore = peg.CalculateScore(DataMessenger.GetInt(IntKey.CurrentScore));
            DataMessenger.SetInt(IntKey.CurrentScore, updatedScore);
            EventMessenger.TriggerEvent(EventKey.ScoreUpdated);
        }
    }
}
