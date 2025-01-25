using System;
using System.Collections.Generic;
using UnityEngine;

public class PegManager : MonoBehaviour
{
    public enum PegDebuffTypes
    {
        Fake, Sticky, DoubleSize, Pop, ReduceBallMass, IncreaseBallMass
    }

    private Dictionary<int, Peg> currentPegs = new();

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.NewPegCreated, NewPegCreated);

        EventMessenger.StartListening(EventKey.ApplyPegDebuff, ApplyDebuff);
        DataMessenger.SetGameObject(GameObjectKey.PegManager, gameObject);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.NewPegCreated, NewPegCreated);

        EventMessenger.StopListening(EventKey.ApplyPegDebuff, ApplyDebuff);
    }
    private void NewPegCreated()
    {
        // Add peg to dictionary
        currentPegs.Add(DataMessenger.GetInt(IntKey.NewPegID), 
            DataMessenger.GetGameObject(GameObjectKey.NewPegObject).GetComponent<Peg>());
    }
    private void ApplyDebuff()
    {
        PegDebuffTypes debuffType = (PegDebuffTypes)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PegDebuffTypes)).Length);

        float debuffChange = UnityEngine.Random.Range(0, 1);

        foreach (var par in currentPegs)
        {

        }

    public int GetBluePegSize()
    {
        return currentPegs.Count;
    }
}
