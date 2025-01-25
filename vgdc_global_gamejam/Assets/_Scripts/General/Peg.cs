using UnityEngine;

public class Peg : MonoBehaviour
{
    [SerializeField] private int scoreToGive;
    private void Hit()
    {
        DataMessenger.SetInt(IntKey.ScoreToAdd, scoreToGive);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit();
    }
}
