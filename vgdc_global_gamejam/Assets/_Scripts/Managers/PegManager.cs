using System.Collections.Generic;
using UnityEngine;

public class PegManager : MonoBehaviour
{
    private Dictionary<int, Peg> currentPegs = new();

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.NewPegCreated, NewPegCreated);
        DataMessenger.SetGameObject(GameObjectKey.PegManager, gameObject);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.NewPegCreated, NewPegCreated);
    }
    private void NewPegCreated()
    {
        // Add peg to dictionary
        currentPegs.Add(DataMessenger.GetInt(IntKey.NewPegID), 
            DataMessenger.GetGameObject(GameObjectKey.NewPegObject).GetComponent<Peg>());
    }

    public int GetBluePegSize()
    {
        return currentPegs.Count;
    }
}
