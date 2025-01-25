using UnityEngine;

public class FakePeg : MonoBehaviour, IDebuf
{
    public void ApplyDebuff(){}

    public void DisableDebuff()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void EnableDebuff()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
