using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentUtil : UIUtil
{
    public override void Awake()
    {

    }
    public override void OnEnable()
    {
        base.OnEnable();

        StartListeningToEvents();
    }
    public override void OnDisable()
    {
        base.OnDisable();

        StopListeningToEvents();
    }
    protected override void Enable()
    {

    }
    protected override void Disable()
    {

    }
}