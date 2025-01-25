using UnityEngine;

public class BasicPeg : MonoBehaviour, IPeg
{
    [SerializeField] private int _scoredAdded;

    public void ApplyEffect()
    {
        // SHould be called from Ball.cs after the score is updated.
        OnDestroyPeg();
    }

    public int CalculateScore(int score)
    {
        return score + _scoredAdded;
    }

    private void OnDestroyPeg()
    {
        Destroy(gameObject);
    }
}
