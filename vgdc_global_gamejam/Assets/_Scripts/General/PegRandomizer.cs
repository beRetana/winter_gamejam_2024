using System.Collections.Generic;
using UnityEngine;

public class PegRandomizer : MonoBehaviour
{
    private static readonly System.Random rng = new();

    [Header("Peg Info")]
    [SerializeField] private int _pointPegCount; // How many point pegs there should be

    [Header("Prefabs")]
    [SerializeField] private GameObject _pointPegPrefab;
    [SerializeField] private GameObject _basicPegPrefab;

    [Header("Randomization")]
    [SerializeField][Range(0, 1)] private float _pegChance; // Chance for a spot on the grid to be a peg
    [SerializeField] private float _pointPegWeight; // Weighted chance for a peg to be a point peg
    [SerializeField] private float _basicPegWeight; // Weighted chance for a peg to be a basic peg
    [SerializeField] private float _maxPositionOffset; // Maximum random offset for each peg position

    [Header("Grid Properties")]
    [SerializeField] private Vector2 _gridTopLeft; // Top left corner of grid to place pegs
    [SerializeField] private Vector2 _gridBottomRight; // Bottom right bounds of peg grid
    [SerializeField] private float _gridInterval; // Vertical/horizontal distance between adjacent pegs

    [SerializeField] private float turretAvoidDistannce; // Prevent pegs from spawning within this distance of the turret

    private void Start()
    {
        CreatePegs();
    }
    private void CreatePegs()
    {
        // Adding 0.05f to account for floating point errors
        int gridWidth = (int)((_gridBottomRight.x - _gridTopLeft.x) / _gridInterval + 1 + 0.05f);
        int gridHeight = (int)((_gridTopLeft.y - _gridBottomRight.y) / _gridInterval + 1 + 0.05f);

        Vector2 turretPosition = DataMessenger.GetVector2(Vector2Key.TurretPosition);

        List<Vector3> pegPositions = new();

        for (int i = 0; i < gridHeight; ++i)
        {
            for (int j = 0; j < gridWidth; ++j)
            {
                // Check if this grid space should have a peg
                if (Random.Range(0.0f, 1.0f) <= _pegChance)
                {
                    Vector3 pos = new Vector3(_gridTopLeft.x + j * _gridInterval + Random.Range(-_maxPositionOffset, _maxPositionOffset), 
                        _gridTopLeft.y - i * _gridInterval + Random.Range(-_maxPositionOffset, _maxPositionOffset));
                    if (Vector2.Distance(pos, turretPosition) > turretAvoidDistannce)
                    {
                        pegPositions.Add(pos);
                    }
                }
            }
        }
        Shuffle(pegPositions);

        bool forcePointPeg = false;
        int pointPegCounter = 0;

        for (int i = 0, len = pegPositions.Count; i < len; ++i)
        {
            GameObject pegPrefab = _basicPegPrefab;
            // Check if all remaining pegs should be point pegs in order to meet quota
            if (i + _pointPegCount - pointPegCounter >= len)
            {
                forcePointPeg = true;
            }

            // If there are already correct number of point pegs, don't make anymore
            if (forcePointPeg || (pointPegCounter < _pointPegCount &&
                Random.Range(0, _pointPegWeight + _basicPegWeight) <= _pointPegWeight))
            {
                pegPrefab = _pointPegPrefab;
                ++pointPegCounter;
            }
            Instantiate(pegPrefab, pegPositions[i], Quaternion.identity, transform);
        }
    }

    /// <summary>
    /// Shuffle contents of a list
    /// </summary>
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
