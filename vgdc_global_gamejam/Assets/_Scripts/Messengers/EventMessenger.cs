using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

// Allows communication of events across AssemblyDefinitions
public class EventMessenger : MonoBehaviour
{

    private Dictionary<string, UnityEvent> eventDictionary;

    private static EventMessenger instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            eventDictionary = new();
        }

    }

    /// <summary>
    /// Associates a function with an event. Put this in OnEnable.
    /// </summary>
    /// <param name="eventName">The name of the event to be listened for. The same name will be used when triggering the event.</param>
    /// <param name="listener">The function to be called when the event is triggered.</param>
    /// <returns></returns>
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Associates a function with an event. Put this in OnEnable.
    /// </summary>
    /// <param name="eventName">The name of the event to be listened for. The same name will be used when triggering the event.</param>
    /// <param name="listener">The function to be called when the event is triggered.</param>
    /// <returns></returns>
    public static void StartListening(EventKey eventName, UnityAction listener)
    {
        StartListening(eventName.ToString(), listener);
    }

    /// <summary>
    /// For every StartListening call, put a corresponding StopListening call in OnDisable too.
    /// </summary>
    /// <param name="eventName">The name of the event.</param>
    /// <param name="listener">The function to stop listening to.</param>
    /// <returns></returns>
    public static void StopListening(string eventName, UnityAction listener)
    {
        if (instance == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// For every StartListening call, put a corresponding StopListening call in OnDisable too.
    /// </summary>
    /// <param name="eventName">The name of the event.</param>
    /// <param name="listener">The function to stop listening to.</param>
    /// <returns></returns>
    public static void StopListening(EventKey eventName, UnityAction listener)
    {
        StopListening(eventName.ToString(), listener);
    }

    /// <summary>
    /// Triggers an event through a given event name. All functions listening to the event will be invoked.
    /// </summary>
    /// <param name="eventName">The name of the event to trigger.</param>
    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
        else
        {
            Debug.Log("EventMessenger does not contain " + eventName);
        }
    }

    /// <summary>
    /// Triggers an event through a given event name. All functions listening to the event will be invoked.
    /// </summary>
    /// <param name="eventName">The name of the event to trigger.</param>
    public static void TriggerEvent(EventKey eventName)
    {
        TriggerEvent(eventName.ToString());
    }
}

public enum EventKey
{
    AddScore,
    ScoreUpdated,
    NewPegCreated,
    RoundEnded,
    AddBall,
    BallCountUpdated,
    DestroyBall,
    ShootBall,
    BallInfoUpdated,
    ApplyDebuff,
    ApplyPegDebuff,
    ApplyBoardDebuff,
    TiltCamera,
    ApplyShotDebuff,
    UpdateBallDebuffs,
    ShowDebuffSelection,
    DebuffSelected,
}