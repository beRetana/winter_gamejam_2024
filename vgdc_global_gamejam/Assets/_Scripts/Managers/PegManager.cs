using System;
using System.Collections.Generic;
using UnityEngine;
using static BoardManager;

public class PegManager : MonoBehaviour
{
    public enum PegDebuffType
    {
        Fake, Sticky, DoubleSize, Pop, ReduceBallMass, IncreaseBallMass
    }

    private Dictionary<int, Peg> currentBluePegs = new();
    private Dictionary<int, Peg> currentOrangePegs = new();

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.NewPegBlueCreated, NewPegBlueCreated);
        EventMessenger.StartListening(EventKey.NewPegOrangeCreated, NewPegOrangeCreated);

        EventMessenger.StartListening(EventKey.ApplyPegDebuff, ApplyDebuff);
        DataMessenger.SetGameObject(GameObjectKey.PegManager, gameObject);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.NewPegBlueCreated, NewPegBlueCreated);
        EventMessenger.StopListening(EventKey.NewPegOrangeCreated, NewPegBlueCreated);

        EventMessenger.StopListening(EventKey.ApplyPegDebuff, ApplyDebuff);
    }
    private void NewPegBlueCreated()
    {
        // Add peg to dictionary
        currentBluePegs.Add(DataMessenger.GetInt(IntKey.NewPegID), 
            DataMessenger.GetGameObject(GameObjectKey.NewPegObject).GetComponent<Peg>());
    }
    private void NewPegOrangeCreated()
    {
        // Add peg to dictionary
        currentOrangePegs.Add(DataMessenger.GetInt(IntKey.NewPegID), 
            DataMessenger.GetGameObject(GameObjectKey.NewPegObject).GetComponent<Peg>());
    }
    private void ApplyDebuff()
    {
        PegDebuffType debuffType = (PegDebuffType)DataMessenger.GetInt(IntKey.DebuffEnumID);
        //(PegDebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PegDebuffType)).Length);

        float debuffChange = UnityEngine.Random.Range(0, 1);

        foreach (var par in currentBluePegs)
        {
            
        }
    }

    public int GetBluePegSize()
    {
        return currentBluePegs.Count;
    }
}
