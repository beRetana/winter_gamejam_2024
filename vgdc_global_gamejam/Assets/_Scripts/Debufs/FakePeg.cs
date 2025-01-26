using UnityEngine;

public class FakePeg : MonoBehaviour, IDebuf
{
    public void ApplyDebuff(){}

    public void DisableDebuff()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }

    public void EnableDebuff()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventMessenger.TriggerEvent(EventKey.FlashCurseOfRa);
    }
}
