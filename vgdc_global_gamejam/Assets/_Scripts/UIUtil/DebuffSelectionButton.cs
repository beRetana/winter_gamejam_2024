using System;
using TMPro;
using UnityEngine;

public class DebuffSelectionButton : ButtonUtil
{
    private TextMeshProUGUI _text;

    private int _id;
    public override void Start()
    {
        base.Start();

        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        string[] info = name.Split();
        _id = int.Parse(info[0]);
        _text.text = info[1].AddSpaces() + ": " + info[2].AddSpaces();

        button.onClick.AddListener(SelectDebuff);
    }
    private void SelectDebuff()
    {
        DataMessenger.SetInt(IntKey.DebuffSelection, _id);
        EventMessenger.TriggerEvent(EventKey.DebuffSelected);
    }
}
