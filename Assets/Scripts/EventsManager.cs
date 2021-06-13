using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public event Action<GameObject> onAgentOffTrigger;
    public void AgentOffTrigger(GameObject obj)
    {
        onAgentOffTrigger?.Invoke(obj);
    }
}