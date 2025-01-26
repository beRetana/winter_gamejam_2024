using System;
using System.Collections.Generic;
using System.Diagnostics;
using Mono.Cecil.Cil;
using UnityEngine;

public class PegManager : MonoBehaviour
{
    public enum PegDebuffType
    {
        Fake, NoBounce, Death
    }

    private Dictionary<int, BasicPeg> currentBluePegs = new();
    private Dictionary<int, PointPeg> currentPointPegs = new();

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.NewBluePegCreated, NewBluePegCreated);
        EventMessenger.StartListening(EventKey.NewPointPegCreated, NewPointPegCreated);
        EventMessenger.StartListening(EventKey.DestroyedBluePeg, OnDestroyedBluePeg);
        EventMessenger.StartListening(EventKey.DestroyedPointPeg, OnDestroyedPointPeg);

        EventMessenger.StartListening(EventKey.RestartGame, ResetPegs);

        EventMessenger.StartListening(EventKey.ApplyPegDebuff, ApplyDebuff);
        DataMessenger.SetGameObject(GameObjectKey.PegManager, gameObject);
    }

    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.NewBluePegCreated, NewBluePegCreated);
        EventMessenger.StopListening(EventKey.NewPointPegCreated, NewBluePegCreated);

        EventMessenger.StopListening(EventKey.RestartGame, ResetPegs);

        EventMessenger.StopListening(EventKey.ApplyPegDebuff, ApplyDebuff);
    }
    private void ResetPegs()
    {
        currentPointPegs.Clear();
        currentBluePegs.Clear();
    }

    private void NewBluePegCreated()
    {
        // Add peg to dictionary
        currentBluePegs.Add(DataMessenger.GetInt(IntKey.NewBluePegID), 
            DataMessenger.GetGameObject(GameObjectKey.NewBluePegObject).GetComponent<BasicPeg>());
    }

    private void NewPointPegCreated()
    {
        // Add peg to dictionary
        currentPointPegs.Add(DataMessenger.GetInt(IntKey.NewPointPegID), 
            DataMessenger.GetGameObject(GameObjectKey.NewPointPegObject).GetComponent<PointPeg>());
    }

    private void OnDestroyedPointPeg()
    {
        currentPointPegs.Remove(DataMessenger.GetInt(IntKey.DestroyedPointPegID));
    }

    private void OnDestroyedBluePeg()
    {
        currentBluePegs.Remove(DataMessenger.GetInt(IntKey.DestroyedBluePegID));
    }

    private void ApplyDebuff()
    {
        PegDebuffType debuffType = (PegDebuffType)DataMessenger.GetInt(IntKey.DebuffEnumID);
        //(PegDebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PegDebuffType)).Length);

        foreach (BasicPeg peg in currentBluePegs.Values)
        {
            if (peg.gameObject.GetComponent<IDebuf>() != null){return;}

            UnityEngine.Debug.Log(peg.gameObject.name);
            switch (debuffType)
            {
                case PegDebuffType.Fake:
                    peg.gameObject.AddComponent<FakePeg>();
                    break;
                case PegDebuffType.NoBounce:
                    peg.gameObject.AddComponent<DebufNoBounce>();
                    break;
                case PegDebuffType.Death:
                    peg.gameObject.AddComponent<DeathBall>();
                    break;
                
            }
            peg.gameObject.AddComponent<FakePeg>();
            peg.ApplyDebuf();
            return;
        }
    }

    public int GetBluePegSize()
    {
        return currentBluePegs.Count;
    }
}
