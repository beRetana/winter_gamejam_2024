using System;
using Unity.VisualScripting;
using UnityEngine;

public class hedgeHogJiggler : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;

    void Update()
    {
        transform.localPosition += new Vector3(0, Mathf.Sin(Time.time) * speed, 0);
    }
}
