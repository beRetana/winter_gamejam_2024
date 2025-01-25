using UnityEngine;

public class BasicPeg : MonoBehaviour, IPeg
{
    [SerializeField] private int _scoredAdded;

    public void ApplyEffect()
    {
        // Plays animation sounds and other effects
    }

    public int CalculateScore(int score)
    {
        return score + _scoredAdded;
    }
}
