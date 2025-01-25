using UnityEngine;

public class DeathBall : IDebuf
{
    public void ApplyDebuff()
    {
        GameObject ball = DataMessenger.GetGameObject(GameObjectKey.PlayerBall);
        DataMessenger.SetBool(BoolKey.IsBallInPlay, false);

        EventMessenger.TriggerEvent(EventKey.RoundEnded);

        Destroy(ball);
    }

    public void DisableDebuff()
    {

    }

    public void EnableDebuff()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

}
