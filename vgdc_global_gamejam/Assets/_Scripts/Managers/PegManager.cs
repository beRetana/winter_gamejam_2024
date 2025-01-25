using System;
using System.Collections.Generic;
using UnityEngine;
using static DebuffManager;

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

        EventMessenger.StartListening(EventKey.ApplyPegDebug, ApplyDebuff);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.NewPegCreated, NewPegCreated);

        EventMessenger.StopListening(EventKey.ApplyPegDebug, ApplyDebuff);
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


    }
}
