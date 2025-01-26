using System;
using UnityEngine;

public class ShotManager : MonoBehaviour
{
    public enum ShotDebuffType {
        ReduceBallMass, IncreaseBallMass,
    }
    [SerializeField] private float ballMassDebuffMultiplier = 2;
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.ApplyShotDebuff, ApplyDebuff);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.ApplyShotDebuff, ApplyDebuff);
    }
    private void Start()
    {
        DataMessenger.SetFloat(FloatKey.BallMassMultiplier, 1);
    }
    private void ApplyDebuff()
    {
        ShotDebuffType debuffType = (ShotDebuffType)DataMessenger.GetInt(IntKey.DebuffEnumID);
            //(ShotDebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ShotDebuffType)).Length);

        switch (debuffType)
        {
            case ShotDebuffType.ReduceBallMass:
                DataMessenger.OperateFloat(FloatKey.BallMassMultiplier, ballMassDebuffMultiplier, '/');
                break;
            case ShotDebuffType.IncreaseBallMass:
                DataMessenger.OperateFloat(FloatKey.BallMassMultiplier, ballMassDebuffMultiplier, '*');
                break;
        }
        EventMessenger.TriggerEvent(EventKey.UpdateBallDebuffs);

        Debug.Log("Applied Shot Debuff: " + debuffType);
    }
}
