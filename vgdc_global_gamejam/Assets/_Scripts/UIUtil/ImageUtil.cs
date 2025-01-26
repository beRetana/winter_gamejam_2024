using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ComponentUtil class for Images
public class ImageUtil : UIComponentUtil
{
    protected Image image;
    public override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
    }
    protected override void Enable()
    {
        image.enabled = true;
    }
    protected override void Disable()
    {
        image.enabled = false;
    }
    protected override bool IsActive()
    {
        return image.enabled;
    }
}