using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
    }
}
