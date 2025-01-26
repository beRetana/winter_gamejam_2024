using UnityEngine;

public class DeathBall : MonoBehaviour, IDebuf
{
    private Color _defaultColor;
    
    public void ApplyDebuff()
    {
        GameObject ball = DataMessenger.GetGameObject(GameObjectKey.PlayerBall);
        DataMessenger.SetBool(BoolKey.IsBallInPlay, false);

        EventMessenger.TriggerEvent(EventKey.RoundEnded);

        Destroy(ball);
    }

    public void DisableDebuff()
    {
        GetComponentInChildren<SpriteRenderer>().color = _defaultColor;
        this.enabled = false;
    }

    public void EnableDebuff()
    {
        _defaultColor = GetComponentInChildren<SpriteRenderer>().color;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

}
