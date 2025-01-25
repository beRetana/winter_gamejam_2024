using UnityEngine;

public class DeathBall : IDebuf
{
    private GameObject ball;

    public void ApplyDebuff()
    {
        
    }

    public void DisableDebuff()
    {
        ball = null;
    }

    public void EnableDebuff()
    {
        ball = GameObject.FindGameObjectWithTag("ball") as GameObject;
    }

}
