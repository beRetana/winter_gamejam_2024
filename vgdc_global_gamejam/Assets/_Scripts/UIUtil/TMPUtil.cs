using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// TextMeshProUGUI extension of ComponentUtil
/// </summary>
public class TMPUtil : UIComponentUtil
{
    protected TextMeshProUGUI text;
    public override void Awake()
    {
        base.Awake();

        text = GetComponent<TextMeshProUGUI>();
    }
    protected override void StartListeningToEvents()
    {
        base.StartListeningToEvents();

        EventMessenger.StartListening("Update" + uiName, UpdateText);
    }
    protected override void StopListeningToEvents()
    {
        base.StopListeningToEvents();

        EventMessenger.StopListening("Update" + uiName, UpdateText);
    }
    protected virtual void UpdateText()
    {
        text.text = DataMessenger.GetString(uiName);
    }
    protected override void Enable()
    {
        base.Enable();

        text.enabled = true;
    }
    protected override void Disable()
    {
        base.Disable();

        text.enabled = false;
    }
    protected override bool IsActive()
    {
        return text.enabled;
    }
}