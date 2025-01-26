using UnityEngine;

public class DebufNoBounce : MonoBehaviour, IDebuf
{
    private PhysicsMaterial2D _physicsMaterial;
    private PhysicsMaterial2D _defaultMaterial;
    private PhysicsMaterial2D _defaultRbMaterial;

    public void ApplyDebuff()
    {
        // Nothing much
    }

    public void DisableDebuff()
    {
        GetComponent<Collider2D>().sharedMaterial = _defaultMaterial;
        GetComponent<Rigidbody2D>().sharedMaterial = _defaultRbMaterial;
    }

    public void EnableDebuff()
    {
        _physicsMaterial = new();
        _physicsMaterial.bounciness = 0;
        _physicsMaterial.bounceCombine = PhysicsMaterialCombine2D.Multiply;
        _defaultMaterial = GetComponent<Collider2D>().sharedMaterial;
        _defaultRbMaterial = GetComponent<Rigidbody2D>().sharedMaterial;
        GetComponent<Collider2D>().sharedMaterial = _physicsMaterial;
        GetComponent<Rigidbody2D>().sharedMaterial = _physicsMaterial;
    }
}
