using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "New Event", fileName = "Event", order = 0)]
public class Events : ScriptableObject
{
    public List<EventListener> eListeners;

    public void Register(EventListener el)
    {
        eListeners.Add(el);
    }

    public void Unregister(EventListener el)
    {
        eListeners.Remove(el);
    }

    public void OnOccur()
    {
        foreach(var el in eListeners)
        {
            el.Invoke();
        }
    }
}
