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
        GetComponent<SpriteRenderer>().color = _defaultColor;
    }

    public void EnableDebuff()
    {
        _defaultColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

}
