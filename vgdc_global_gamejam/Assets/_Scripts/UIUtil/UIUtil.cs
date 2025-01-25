using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General utility for UI components
/// </summary>
public class UIUtil : MonoBehaviour
{
    [SerializeField] protected bool disableOnStart; // Whether Disable() should be called in start

    [SerializeField] protected string uiName;
    public virtual void Awake()
    {

    }
    public virtual void OnEnable()
    {

    }
    public virtual void OnDisable()
    {

    }
    public virtual void Start()
    {
        if (disableOnStart)
        {
            Disable();
        }
    }
    protected virtual void StartListeningToEvents()
    {

    }
    protected virtual void StopListeningToEvents()
    {

    }

    // Enable & Disable: Overridden by ComponentUtils to only affect active state of components, not entire Gameobject
    protected virtual void Enable()
    {
        gameObject.SetActive(true);
    }
    protected virtual void Disable()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Check whether this UI is active or not. Overridden by ComponentUtils to return active state of comopnents only.
    /// </summary>
    /// <returns></returns>
    protected virtual bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}